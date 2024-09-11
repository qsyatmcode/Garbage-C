namespace CCCP.Constructs;

public class ProductionRule
{
    private Pattern[] _rule;

    public ProductionRule(params Pattern[] rule)
    {
        if(rule.Length == 0)
            throw new ArgumentException("Rule must not be empty");
        _rule = rule;
    }
}