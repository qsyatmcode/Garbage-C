using GarbageC.BackEnd;

namespace GarbageC.FrontEnd;

public class ProgramListener : cprogramBaseListener
{
    private ProgramVisitor _visitor;
    
    public ProgramListener(ProgramVisitor visitor) : base()
    {
        _visitor = visitor;
    }
    
    public override void ExitStatement(cprogramParser.StatementContext context) // adding to last function in _generator.Functions
    {
       Generator.Emitter.Return();
    }

    public override void EnterStatement(cprogramParser.StatementContext context)
    {
        Generator.Emitter.Expression(_visitor.Visit(context.exp()));
    }

    public override void EnterFunction(cprogramParser.FunctionContext context)
    {
        Generator.AddFunction(context.ID().GetText());
    }
}