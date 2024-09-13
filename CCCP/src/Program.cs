using System.Text.RegularExpressions;

namespace CCCP;
using Constructs;
using static Result<List<string>>;

class Program
{
    private static readonly Dictionary<PatternType, string> Patterns = new Dictionary<PatternType, string>()
    {
        { (PatternType)0, "{" },
        { (PatternType)1, "}" },
        { (PatternType)2, @"\(" },
        { (PatternType)3, @"\)" },
        { (PatternType)4, ";" },
        { (PatternType)5, "int" },
        { (PatternType)6, "return" },
        { (PatternType)7, @"[a-zA-Z]\w*" },
        { (PatternType)8, "[0-9]+" }
    };
    
    static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: ./CCCP.exe <input>");
            return;
        }

        var lex = Lex(args[0]).GetValueOrThrow();

        List<Pattern> patterns = new List<Pattern>();
        
        foreach (var l in lex)
        {
            var t = DetermineMatchType(l);
            patterns.Add(CreatePattern(l, t));
        }

        foreach (var pat in patterns)
        {
            Console.WriteLine(pat);
        }
        
        // TODO: Parser.Parse(patterns);
        
        Console.ReadKey();
    }

    private static Result<List<string>> Lex(string path)
    {
        //if (File.Exists(path)) // this shit is not working!
        //    throw new ArgumentException("File is not exists");

        string commonPattern = "";
        for (int i = 0; i < (int)PatternType.Count; i++)
        {
            commonPattern += Patterns[ (PatternType)i ]; // so many allocations :))
            if (i != (int)PatternType.Count - 1)
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

    static PatternType DetermineMatchType(string match) => 
        (from pattern in Patterns 
            where Regex.Count(match, pattern.Value) != 0 
            orderby (int)pattern.Key 
            select pattern.Key).First();

    static Pattern CreatePattern(string match, PatternType type)
    {
        if ((int)type >= (int)PatternType.Identifier)
        {
            return new NonTerminalPattern(null, (NonTerminalPatternType)type, match);
        }
        return new TerminalPattern((TerminalPatternType)type, match);
    }
}