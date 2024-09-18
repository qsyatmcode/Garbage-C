namespace GarbageC.Constructs;

public class ASTNode
{
    public List<ASTNode> Childrens { get; set; } = new List<ASTNode>();
    public List<Lexeme> Tokens { get; set; } = new List<Lexeme>();
    public RuleType NodeType { get; set; }
}