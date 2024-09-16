namespace GarbageC.Constructs;

public class AST
{
    public class ASTNode
    {
        public List<ASTNode> Childrens { get; set; } = new List<ASTNode>();
        public List<Lexem> Tokens { get; set; } = new List<Lexem>();
        public RuleType NodeType { get; set; }
    }

    public List<ASTNode> Nodes { get; set; } = new List<ASTNode>();
}