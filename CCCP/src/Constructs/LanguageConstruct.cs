namespace CCCP.Constructs;

public abstract class LanguageConstruct<TDerived> where TDerived : LanguageConstruct<TDerived>
{
    public abstract TDerived FromString();
    protected abstract void Parse();
}