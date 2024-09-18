using System.Data;
using System.Formats.Asn1;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using GarbageC.CodeGeneration;

namespace GarbageC;
using Constructs;
using static Result<List<string>>;

class Program
{
    private static readonly Dictionary<LexemeType, string> Patterns = new Dictionary<LexemeType, string>()
    {
        { (LexemeType)0, "{" },
        { (LexemeType)1, "}" },
        { (LexemeType)2, @"\(" },
        { (LexemeType)3, @"\)" },
        { (LexemeType)4, ";" },
        { (LexemeType)5, "int" },
        { (LexemeType)6, "return" },
        { (LexemeType)7, @"[a-zA-Z]\w*" },
        { (LexemeType)8, "[0-9]+" }
    };
    
    private static Dictionary<RuleType, List<Lexeme[]>> ProductionRules = new Dictionary<RuleType, List<Lexeme[]>>()
    {
        {
            RuleType.Expression,
            new List<Lexeme[]>()
            {
                new Lexeme[] { LexemeOf(LexemeType.IntegerLiteral) }
            }
        },
        {
            RuleType.Statement,
            new List<Lexeme[]>() { 
                new Lexeme[] { LexemeOf(LexemeType.ReturnKeyword), LexemeOf(LexemeType.IntegerLiteral, true, RuleType.Expression), LexemeOf(LexemeType.Semicolon) } 
            }
        },
        {
            RuleType.Function,
            new List<Lexeme[]>()
            {
                new Lexeme[]
                {
                    LexemeOf(LexemeType.IntKeyword), LexemeOf(LexemeType.Identifier), LexemeOf(LexemeType.OpenParenthesis), LexemeOf(LexemeType.CloseParenthesis),
                    LexemeOf(LexemeType.OpenBraces), LexemeOf(LexemeType.IntegerLiteral, true, RuleType.Statement), LexemeOf(LexemeType.CloseBraces),
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

        var lexemes = new List<Lexeme>();
        
        foreach (var l in lex)
        {
            var t = DetermineMatchType(l);
            lexemes.Add(CreateLexem(l, t));
        }

        foreach (var pat in lexemes)
        {
            Console.WriteLine(pat);
        }
        
        var d = Parse(lexemes).GetValueOrThrow();

        PrintAST(d);
        
        Generator.Generate(d, "test");
    }

    private static void PrintAST(ASTNode root, int tabs = 0) // TEMP
    {
        Tabs();
        Console.WriteLine($"Type: {root.NodeType}");
        Tabs();
        Console.WriteLine("Tokens: {");

        foreach (var token in root.Tokens)
        {
            Tabs();
            Console.WriteLine($":\t{token}");
        }
        
        Tabs();
        Console.WriteLine("}");
        Tabs();
        Console.WriteLine("Childrens:");

        foreach (var node in root.Childrens)
        {
            PrintAST(node, tabs + 1);
        }

        void Tabs()
        {
            for (int i = 0; i < tabs; i++)
            {
                Console.Write("\t");
            }
        }
    }
    
    private static Result<ASTNode> Parse(List<Lexeme> lexemes)
    {
        ASTNode result = new ASTNode();

        int index = 0;
        
        ParseLexem(ref index, result);
        result.NodeType = RuleType.Program;

        return Result<ASTNode>.Ok(result);

        void ParseLexem(ref int index, ASTNode root, RuleType? ruleType = null)
        {
            var current = new ASTNode();
            var tokens = new List<Lexeme>() { Capacity = 8 };
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

                    //if (root.Childrens.Count == 0)
                    //{
                    //    throw new ApplicationException($"No matching rules  Index: {index},\n Lexeme: {lexemes[index]};\n");
                    //}
                }
            }

            void RuleParse(ref int index, Lexeme[] rule, RuleType type)
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

    public static Lexeme LexemeOf(LexemeType type, bool recursive = false, RuleType? recursiveRule = null)
    {
        return new Lexeme(type, Patterns[type], recursive, recursiveRule);
    }
    
    private static Result<List<string>> Lex(string path)
    {
        //if (File.Exists(path)) // this shit is not working!
        //    throw new ArgumentException("File is not exists");

        string commonPattern = "";
        for (int i = 0; i < (int)LexemeType.Count; i++)
        {
            commonPattern += Patterns[ (LexemeType)i ]; // so many allocations :))
            if (i != (int)LexemeType.Count - 1)
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

    static LexemeType DetermineMatchType(string match) => 
        (from pattern in Patterns 
            where Regex.Count(match, pattern.Value) != 0 
            orderby (int)pattern.Key 
            select pattern.Key).First();

    static Lexeme CreateLexem(string value, LexemeType type)
    {
        return new Lexeme(type, value, false, null);
    }
}