namespace CCCP.Constructs;

public enum PatternType
{
    OpenBraces,
    CloseBraces,
    OpenParenthesis,
    CloseParenthesis,
    Semicolon,
    IntKw,
    ReturnKw,
    
    // Non-terminal:
    Identifier,
    IntegerLiteral,
    
    Count
}

public enum NonTerminalPatternType
{
    Identifier = (int)PatternType.Identifier, 
    IntegerLiteral = (int)PatternType.IntegerLiteral,
    Count
}

public enum TerminalPatternType
{
    OpenBraces,
    CloseBraces,
    OpenParenthesis,
    CloseParenthesis,
    Semicolon,
    IntKw,
    ReturnKw,
    Count
}

public record Pattern;
public record TerminalPattern(TerminalPatternType Type, string Value) : Pattern;
public record NonTerminalPattern(TerminalPattern[]? Sequence, NonTerminalPatternType Type, string? Piece) : Pattern;

public static class TerminalSymbols
{
    public static readonly List<TerminalPattern> Symbols;
    static TerminalSymbols()
    {
        Symbols = new List<TerminalPattern>()
        {
            new TerminalPattern(TerminalPatternType.OpenBraces, "{"),
            new TerminalPattern(TerminalPatternType.CloseBraces, "}"),
            new TerminalPattern(TerminalPatternType.OpenParenthesis, "("),
            new TerminalPattern(TerminalPatternType.CloseParenthesis, ")"),
            new TerminalPattern(TerminalPatternType.Semicolon, ","),
            new TerminalPattern(TerminalPatternType.IntKw, "int"),
            new TerminalPattern(TerminalPatternType.ReturnKw, "return"),
        };
    }
}