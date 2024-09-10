namespace CCCP.Constructs;

public interface IProductionRule<TDerived> where TDerived : IProductionRule<TDerived>
{
    public TDerived FromString();
}