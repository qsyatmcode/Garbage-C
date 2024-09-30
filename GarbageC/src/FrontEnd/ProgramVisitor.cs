namespace GarbageC.FrontEnd;

public class ProgramVisitor : cprogramBaseVisitor<int>
{
    public override int VisitLiteral(cprogramParser.LiteralContext context)
    {
        return int.Parse(context.INT().GetText());
    }

    public override int VisitBWShift(cprogramParser.BWShiftContext context)
    {
        int a = Visit(context.exp(0));
        int b = Visit(context.exp(1));
        if (context.op.Type == cprogramParser.BWRS) return a >> b;
        return a << b;
    }

    public override int VisitBWAnd(cprogramParser.BWAndContext context)
    {
        int a = Visit(context.exp(0));
        int b = Visit(context.exp(1));
        return a & b;
        
    }
    
    public override int VisitBWOr(cprogramParser.BWOrContext context)
    {
        int a = Visit(context.exp(0));
        int b = Visit(context.exp(1));
        return a | b;
    }

    public override int VisitBWXor(cprogramParser.BWXorContext context)
    {
        int a = Visit(context.exp(0));
        int b = Visit(context.exp(1));
        return a ^ b;
    }

    public override int VisitUnary(cprogramParser.UnaryContext context) // 6.5.3.3
    {
        switch (context.op.Type)
        {
            case cprogramParser.SUB: // Arithmetic negation
                return -Visit(context.exp());
            case cprogramParser.BWC: // Bitwise complement
                return ~Visit(context.exp());
            case cprogramParser.LNT: // Logical NOT
                return Visit(context.exp()) != 0 ? 0 : 1;
            case cprogramParser.ADD: // Add
                return Visit(context.exp()); // Todo: integer promotion
        }

        return -1;
    }

    public override int VisitLogical(cprogramParser.LogicalContext context)
    {
        bool a = Visit(context.exp(0)) != 0;
        if (a) return 1; // Short-Circuit
        bool b = Visit(context.exp(1)) != 0;
        if (context.op.Type == cprogramParser.OR) return a || b ? 1 : 0;
        return a && b ? 1 : 0;
    }

    public override int VisitRelational(cprogramParser.RelationalContext context)
    {
        int a = Visit(context.exp(0));
        int b = Visit(context.exp(1));
        switch (context.op.Type)
        {
            case cprogramParser.RGR: // Greater
                return a > b ? 1 : 0;
            case cprogramParser.RLS: // Less
                return a < b ? 1 : 0;
            case cprogramParser.RGREQ: // Greater or equals
                return a >= b ? 1 : 0;
            case cprogramParser.RLSEQ: // Less or equals
                return a <= b ? 1 : 0;
        }

        return 42;
    }

    public override int VisitEquality(cprogramParser.EqualityContext context)
    {
        int a = Visit(context.exp(0));
        int b = Visit(context.exp(1));
        if (context.op.Type == cprogramParser.EQLS) return a == b ? 1 : 0;
        return a != b ? 1 : 0;
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
        if (context.op.Type == cprogramParser.DIVR) return a % b;
        return a / b;
    }

    public override int VisitParens(cprogramParser.ParensContext context)
    {
        return Visit(context.exp());
    }
}