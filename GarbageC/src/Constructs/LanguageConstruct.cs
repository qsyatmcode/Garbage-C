namespace GarbageC.Constructs;

public abstract class LanguageConstruct<TDerived> where TDerived : LanguageConstruct<TDerived>
{
    private ProductionRule[] _productionRules;
    public abstract TDerived FromString();
    protected abstract void Parse();
}