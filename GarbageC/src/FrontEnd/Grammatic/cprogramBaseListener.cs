//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.2
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from cprogram.g4 by ANTLR 4.13.2

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419


using Antlr4.Runtime.Misc;
using IErrorNode = Antlr4.Runtime.Tree.IErrorNode;
using ITerminalNode = Antlr4.Runtime.Tree.ITerminalNode;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;

/// <summary>
/// This class provides an empty implementation of <see cref="IcprogramListener"/>,
/// which can be extended to create a listener which only needs to handle a subset
/// of the available methods.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.2")]
[System.Diagnostics.DebuggerNonUserCode]
[System.CLSCompliant(false)]
public partial class cprogramBaseListener : IcprogramListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="cprogramParser.program"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterProgram([NotNull] cprogramParser.ProgramContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="cprogramParser.program"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitProgram([NotNull] cprogramParser.ProgramContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="cprogramParser.function"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterFunction([NotNull] cprogramParser.FunctionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="cprogramParser.function"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitFunction([NotNull] cprogramParser.FunctionContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="cprogramParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterStatement([NotNull] cprogramParser.StatementContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="cprogramParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitStatement([NotNull] cprogramParser.StatementContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>AddSub</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAddSub([NotNull] cprogramParser.AddSubContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>AddSub</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAddSub([NotNull] cprogramParser.AddSubContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>Parens</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterParens([NotNull] cprogramParser.ParensContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>Parens</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitParens([NotNull] cprogramParser.ParensContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>Literal</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterLiteral([NotNull] cprogramParser.LiteralContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>Literal</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitLiteral([NotNull] cprogramParser.LiteralContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>BWOr</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBWOr([NotNull] cprogramParser.BWOrContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>BWOr</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBWOr([NotNull] cprogramParser.BWOrContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>Relational</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterRelational([NotNull] cprogramParser.RelationalContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>Relational</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitRelational([NotNull] cprogramParser.RelationalContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>BWAnd</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBWAnd([NotNull] cprogramParser.BWAndContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>BWAnd</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBWAnd([NotNull] cprogramParser.BWAndContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>Logical</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterLogical([NotNull] cprogramParser.LogicalContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>Logical</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitLogical([NotNull] cprogramParser.LogicalContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>Equality</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterEquality([NotNull] cprogramParser.EqualityContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>Equality</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitEquality([NotNull] cprogramParser.EqualityContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>BWXor</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBWXor([NotNull] cprogramParser.BWXorContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>BWXor</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBWXor([NotNull] cprogramParser.BWXorContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>Unary</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterUnary([NotNull] cprogramParser.UnaryContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>Unary</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitUnary([NotNull] cprogramParser.UnaryContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>BWShift</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBWShift([NotNull] cprogramParser.BWShiftContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>BWShift</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBWShift([NotNull] cprogramParser.BWShiftContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>MultDiv</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterMultDiv([NotNull] cprogramParser.MultDivContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>MultDiv</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitMultDiv([NotNull] cprogramParser.MultDivContext context) { }

	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void EnterEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void ExitEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitTerminal([NotNull] ITerminalNode node) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitErrorNode([NotNull] IErrorNode node) { }
}
