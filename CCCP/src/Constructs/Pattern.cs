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
    Identifier,
    IntegerLiteral,
    Count
}

public record struct Pattern(PatternType type, string value);