namespace GarbageC.Constructs;

public enum RuleType
{
    Statement,
    Function,
    Expression,
    Program,
}

public enum LexemType
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

public record Lexem(LexemType Type, string Value, bool Recursive, RuleType? RecursiveRule);