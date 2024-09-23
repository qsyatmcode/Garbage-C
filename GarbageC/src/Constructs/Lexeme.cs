namespace GarbageC.Constructs;

public enum RuleType
{
    Statement,
    Function,
    Expression,
    Factor,
    Term,
    TermRepit,
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
    Minus,
    BitwiseComplement,
    LogicalNegation,
    Addition,
    Multiplication,
    Division,
    Count
}

public record Lexeme(LexemeType Type, string Value, bool Recursive, RuleType? RecursiveRule, bool IndexSkip);