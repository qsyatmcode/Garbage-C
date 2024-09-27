namespace GarbageC.FrontEnd;

public class ProgramVisitor : cprogramBaseVisitor<int>
{
    public override int VisitLiteral(cprogramParser.LiteralContext context)
    {
        return int.Parse(context.INT().GetText());
    }

    public override int VisitUnary(cprogramParser.UnaryContext context)
    {
        switch (context.op.Type)
        {
            case cprogramParser.SUB: // Arithmetic negation
                return -Visit(context.exp());
            case cprogramParser.BWC: // Bitwise complement
                return ~Visit(context.exp());
            case cprogramParser.LNT: // Logical NOT
                return Visit(context.exp()) != 0 ? 0 : 1;
        }

        return -1;
    }

    public override int VisitAddSub(cprogramParser.AddSubContext context)
    {
        int a = Visit(context.exp(0));
        int b = Visit(context.exp(1));
        if (context.op.Type == cprogramParser.ADD) return a + b;
        return a - b;
    }

    public override int VisitMultDiv(cprogramParser.MultDivContext context)
    {
        int a = Visit(context.exp(0));
        int b = Visit(context.exp(1));
        if (context.op.Type == cprogramParser.MUL) return a * b;
        return a / b;
    }

    public override int VisitParens(cprogramParser.ParensContext context)
    {
        return Visit(context.exp());
    }
}