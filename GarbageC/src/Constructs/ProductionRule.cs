namespace GarbageC.Constructs;

public class ProductionRule
{
    private TerminalPattern[] _rule;

    public ProductionRule(params TerminalPattern[] rule)
    {
        if(rule.Length == 0)
            throw new ArgumentException("Rule must not be empty");
        _rule = rule;
    }
}