using System.Text.RegularExpressions;

namespace CCCP;

class Program
{
    enum PatternType
    {
        OpenBraces,
        CloseBraces,
        OpenParenthesis,
        CloseParenthesis,
        Semicolon,
        IntKw,
        ReturnKw,
        Identifier,
        IntegerLiteral,
        Count
    }

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
    
    static int Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: ./CCCP.exe <input>");
            return 1;
        }

        var lex = Lex(args[0]);
        int result = lex.Count;

        Console.WriteLine("\nTokens:");
        foreach (var l in lex)
        {
            Console.WriteLine($"{l}");
        }

        Console.WriteLine("\nCount:");
        Console.WriteLine(result);
        
        Console.ReadKey();
        
        return result;
    }

    static private List<string> Lex(string path)
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

        return result;
    }
}