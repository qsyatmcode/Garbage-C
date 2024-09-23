using System.Text.RegularExpressions;

namespace GarbageC.FrontEnd;

using GarbageC.Constructs;

using static Result<List<string>>;

public static class LexicalParser
{
    public static readonly Dictionary<LexemeType, string> Patterns = new Dictionary<LexemeType, string>()
    {
        { LexemeType.OpenBraces, "{" },
        { LexemeType.CloseBraces, "}" },
        { LexemeType.OpenParenthesis, @"\(" },
        { LexemeType.CloseParenthesis, @"\)" },
        { LexemeType.Semicolon, ";" },
        { LexemeType.IntKeyword, "int" },
        { LexemeType.ReturnKeyword, "return" },
        { LexemeType.Identifier, @"[a-zA-Z]\w*" },
        { LexemeType.IntegerLiteral, "[0-9]+" },
        { LexemeType.Minus, "-" },
        { LexemeType.BitwiseComplement, "~" },
        { LexemeType.LogicalNegation, "!" },
        { LexemeType.Addition, @"\+" },
        { LexemeType.Multiplication, @"\*" },
        { LexemeType.Division, "/" },
    };
    
    public static Result<List<string>> Parse(string path)
    {
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
}