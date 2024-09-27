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
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="cprogramParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.2")]
[System.CLSCompliant(false)]
public interface IcprogramListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="cprogramParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterProgram([NotNull] cprogramParser.ProgramContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="cprogramParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitProgram([NotNull] cprogramParser.ProgramContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="cprogramParser.function"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunction([NotNull] cprogramParser.FunctionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="cprogramParser.function"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunction([NotNull] cprogramParser.FunctionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="cprogramParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStatement([NotNull] cprogramParser.StatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="cprogramParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStatement([NotNull] cprogramParser.StatementContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>AddSub</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAddSub([NotNull] cprogramParser.AddSubContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>AddSub</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAddSub([NotNull] cprogramParser.AddSubContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>Parens</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParens([NotNull] cprogramParser.ParensContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Parens</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParens([NotNull] cprogramParser.ParensContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>Literal</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLiteral([NotNull] cprogramParser.LiteralContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Literal</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLiteral([NotNull] cprogramParser.LiteralContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>Unary</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterUnary([NotNull] cprogramParser.UnaryContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Unary</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitUnary([NotNull] cprogramParser.UnaryContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>MultDiv</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMultDiv([NotNull] cprogramParser.MultDivContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>MultDiv</c>
	/// labeled alternative in <see cref="cprogramParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMultDiv([NotNull] cprogramParser.MultDivContext context);
}
