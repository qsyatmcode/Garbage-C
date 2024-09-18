namespace GarbageC.Constructs;

public enum RuleType
{
    Statement,
    Function,
    Expression,
    Program,
}

public enum LexemeType
{
    OpenBraces,
    CloseBraces,
    OpenParenthesis,
    CloseParenthesis,
    Semicolon,
    IntKeyword,
    ReturnKeyword,
    Identifier,
    IntegerLiteral,
    Count
}

public record Lexeme(LexemeType Type, string Value, bool Recursive, RuleType? RecursiveRule);