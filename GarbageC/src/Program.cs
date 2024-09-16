using System.Formats.Asn1;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace GarbageC;
using Constructs;
using static Result<List<string>>;

class Program
{
    private static readonly Dictionary<LexemType, string> Patterns = new Dictionary<LexemType, string>()
    {
        { (LexemType)0, "{" },
        { (LexemType)1, "}" },
        { (LexemType)2, @"\(" },
        { (LexemType)3, @"\)" },
        { (LexemType)4, ";" },
        { (LexemType)5, "int" },
        { (LexemType)6, "return" },
        { (LexemType)7, @"[a-zA-Z]\w*" },
        { (LexemType)8, "[0-9]+" }
    };
    
    private static Dictionary<RuleType, List<Lexem[]>> ProductionRules = new Dictionary<RuleType, List<Lexem[]>>()
    {
        {
            RuleType.Expression,
            new List<Lexem[]>()
            {
                new Lexem[] { LexemeOf(LexemType.IntegerLiteral) }
            }
        },
        {
            RuleType.Statement,
            new List<Lexem[]>() { 
                new Lexem[] { LexemeOf(LexemType.ReturnKeyword), LexemeOf(LexemType.IntegerLiteral, true, RuleType.Expression), LexemeOf(LexemType.Semicolon) } 
            }
        },
        {
            RuleType.Function,
            new List<Lexem[]>()
            {
                new Lexem[]
                {
                    LexemeOf(LexemType.IntKeyword), LexemeOf(LexemType.Identifier), LexemeOf(LexemType.OpenParenthesis), LexemeOf(LexemType.CloseParenthesis),
                    LexemeOf(LexemType.OpenBraces), LexemeOf(LexemType.IntegerLiteral, true, RuleType.Statement), LexemeOf(LexemType.CloseBraces),
                }
            }
        },
    };
    
    static void Main(string[] args)
    {
        if (Patterns == null)
        {
            Console.WriteLine("NULL NULL");
            throw new Exception("Patterns is null");
        }
        
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: ./.exe <input>");
            return;
        }

        var lex = Lex(args[0]).GetValueOrThrow();

        var lexemes = new List<Lexem>();
        
        foreach (var l in lex)
        {
            var t = DetermineMatchType(l);
            lexemes.Add(CreateLexem(l, t));
        }

        foreach (var pat in lexemes)
        {
            Console.WriteLine(pat);
        }
        
        // TODO: Parser.Parse(patterns);
        
        var d = Parse(lexemes).GetValueOrThrow();
        
        
        Console.ReadKey();
    }

    private static Result<AST.ASTNode> Parse(List<Lexem> lexemes)
    {
        AST.ASTNode result = new AST.ASTNode();

        List<Lexem> context = new List<Lexem>(); // Для сохранения контекста, если нужно проверить больше 1 лексемы
        int index = 0;
        
        ParseLexem(ref index, result);
        result.NodeType = RuleType.Program;

        return Result<AST.ASTNode>.Ok(result);

        void ParseLexem(ref int index, AST.ASTNode root, RuleType? ruleType = null)
        {
            var current = new AST.ASTNode();
            var tokens = new List<Lexem>() { Capacity = 8 };
            int matches = 0;
            int tempIndex = 0;

            if (ruleType != null)
            {
                foreach (var rule in ProductionRules[ruleType ?? 0])
                {
                    RuleParse(ref index, rule, ruleType ?? RuleType.Program);
                }
            }
            else
            {
                foreach (var rules in ProductionRules)
                {
                    foreach (var rule in rules.Value)
                    {
                        RuleParse(ref index, rule, rules.Key);
                    }
                }
            }

            void RuleParse(ref int index, Lexem[] rule, RuleType type)
            {
                matches = 0;
                tokens.Clear();
                tempIndex = index;
                for (int i = 0; i < rule.Length; i++)
                {
                    if (rule[i].Recursive) // recursive is true
                    {
                        ParseLexem(ref index, current, rule[i].RecursiveRule);
                        matches++;
                        i++;
                    }
                    
                    if (Regex.IsMatch(lexemes[index].Value, rule[i].Value))
                    {
                        tokens.Add(lexemes[index]);
                        matches++;
                        index++;
                    }
                    else
                    {
                        index = tempIndex;
                        return;
                        //break;
                    }
                }
                if (matches == rule.Length)
                {
                    current.NodeType = type;
                    current.Tokens = tokens;
                    root.Childrens.Add(current);
                }
            }
        }
    }

    public static Lexem LexemeOf(LexemType type, bool recursive = false, RuleType? recursiveRule = null)
    {
        return new Lexem(type, Patterns[type], recursive, recursiveRule);
    }
    
    private static Result<List<string>> Lex(string path)
    {
        //if (File.Exists(path)) // this shit is not working!
        //    throw new ArgumentException("File is not exists");

        string commonPattern = "";
        for (int i = 0; i < (int)LexemType.Count; i++)
        {
            commonPattern += Patterns[ (LexemType)i ]; // so many allocations :))
            if (i != (int)LexemType.Count - 1)
                commonPattern += "|";
        }
        
        var result = new List<string>();
        
        string text = File.ReadAllText(path);

        MatchCollection matches;
        
        matches = Regex.Matches(text, commonPattern);
            
        foreach (var match in matches)
        {
            if (match is Match m)
            {
                result.Add(m.Value);
            }
        }
;
        return result.Count == 0 ? Error("No matches found.") : Ok(result);
    }

    static LexemType DetermineMatchType(string match) => 
        (from pattern in Patterns 
            where Regex.Count(match, pattern.Value) != 0 
            orderby (int)pattern.Key 
            select pattern.Key).First();

    static Lexem CreateLexem(string value, LexemType type)
    {
        return new Lexem(type, value, false, null);
    }
}