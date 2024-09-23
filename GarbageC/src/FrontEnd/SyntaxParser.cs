using System.Text.RegularExpressions;
using ASTNode = GarbageC.Constructs.ASTNode;

namespace GarbageC.FrontEnd;

using GarbageC.Constructs;

public static class SyntaxParser
{
    private static Lexeme LexemeOf(LexemeType type, bool recursive = false, RuleType? recursiveRule = null, bool indexSkip = false)
    {
        return new Lexeme(type, LexicalParser.Patterns[type], recursive, recursiveRule, indexSkip);
    }
    
    private static readonly Dictionary<RuleType, List<Lexeme[]>> _productionRules = new Dictionary<RuleType, List<Lexeme[]>>()
    {
        {
            RuleType.Function,
            new List<Lexeme[]>()
            {
                new Lexeme[]
                {
                    LexemeOf(LexemeType.IntKeyword), LexemeOf(LexemeType.Identifier), LexemeOf(LexemeType.OpenParenthesis), LexemeOf(LexemeType.CloseParenthesis),
                    LexemeOf(LexemeType.OpenBraces), LexemeOf(LexemeType.IntegerLiteral, true, RuleType.Statement), LexemeOf(LexemeType.CloseBraces),
                }
            }
        },
        {
            RuleType.Factor,
            new List<Lexeme[]>
            {
                new Lexeme[] { LexemeOf(LexemeType.IntegerLiteral) },
                    
                new Lexeme[] { LexemeOf(LexemeType.OpenParenthesis), LexemeOf(LexemeType.IntegerLiteral, true, RuleType.Expression), LexemeOf(LexemeType.CloseParenthesis) },
                
                new Lexeme[] { LexemeOf(LexemeType.LogicalNegation), LexemeOf(LexemeType.IntegerLiteral, true, RuleType.Factor) },
                new Lexeme[] { LexemeOf(LexemeType.Minus), LexemeOf(LexemeType.IntegerLiteral, true, RuleType.Factor) },
                new Lexeme[] { LexemeOf(LexemeType.BitwiseComplement), LexemeOf(LexemeType.IntegerLiteral, true, RuleType.Factor) },
                
            }
        },
        {
            RuleType.TermRepit,
            new List<Lexeme[]>
            {
                new Lexeme[] { LexemeOf(LexemeType.Multiplication), LexemeOf(LexemeType.IntegerLiteral, true, RuleType.Term) },
                new Lexeme[] { LexemeOf(LexemeType.Division), LexemeOf(LexemeType.IntegerLiteral, true, RuleType.Term) },
                
            }
        },
        {
            RuleType.Term,
            new List<Lexeme[]>
            {
                new Lexeme[] { LexemeOf(LexemeType.IntegerLiteral, true, RuleType.Factor), LexemeOf(LexemeType.IntegerLiteral, true, RuleType.TermRepit) },
                new Lexeme[] { LexemeOf(LexemeType.IntegerLiteral, true, RuleType.Factor), LexemeOf(LexemeType.IntegerLiteral, true, RuleType.TermRepit) },
                new Lexeme[] { LexemeOf(LexemeType.IntegerLiteral, true, RuleType.Factor) },
            }
        },
        {
            RuleType.ExpressionRepit,
            new List<Lexeme[]>()
            {
                new Lexeme[] { LexemeOf(LexemeType.Addition), LexemeOf(LexemeType.IntegerLiteral, true, RuleType.Expression) },
                new Lexeme[] {  LexemeOf(LexemeType.Minus), LexemeOf(LexemeType.IntegerLiteral, true, RuleType.Expression) },
            }
        },
        {
            RuleType.Expression,
            new List<Lexeme[]>()
            {
                new Lexeme[] { LexemeOf(LexemeType.IntegerLiteral, true, RuleType.Term), LexemeOf(LexemeType.IntegerLiteral, true, RuleType.ExpressionRepit) },
                new Lexeme[] { LexemeOf(LexemeType.IntegerLiteral, true, RuleType.Term), LexemeOf(LexemeType.IntegerLiteral, true, RuleType.ExpressionRepit) },
                new Lexeme[] { LexemeOf(LexemeType.IntegerLiteral, true, RuleType.Term) },
                
            }
        },
        {
            RuleType.Statement,
            new List<Lexeme[]>() { 
                new Lexeme[] { LexemeOf(LexemeType.ReturnKeyword), LexemeOf(LexemeType.IntegerLiteral, true, RuleType.Expression), LexemeOf(LexemeType.Semicolon) } 
            }
        },
    };
    
    public static Result<ASTNode> Parse(List<Lexeme> lexemes)
    {
        ASTNode programNode = new ASTNode();
        programNode.NodeType = RuleType.Program;
        int index = 0;
        Program(programNode);

        void Program(ASTNode root) {
            foreach (var production in _productionRules)
            {
                if (production.Key == RuleType.Function)
                    AnalyseProduction(root, RuleType.Function);
            }
        }
        
        bool AnalyseProduction(ASTNode root, RuleType ruleType = RuleType.Function)
        {
            ASTNode currentNode = new ASTNode();
            int tempIndex = index;
            
            // ANALYSE PRODUCTIONS
            foreach (var prule in _productionRules[ruleType])
            {
                // BACKTRACE
                index = tempIndex; 
                currentNode.Childrens.Clear();
                
                // ANALYSE PRODUCTION
                for(int i = 0; i < prule.Length; )
                {
                    // IF RULE IS NON-TERMINAL (RECURSIVE)
                    if (prule[i].RecursiveRule != null)
                    {
                        RuleType targetRecursiveRule = prule[i].RecursiveRule ?? RuleType.Program;
                        bool success = false;
                        
                        if (prule[i].RecursiveRule == RuleType.ExpressionRepit
                            || prule[i].RecursiveRule == RuleType.TermRepit) // IF IT IS EBNF's { } repeating
                        {
                            success = AnalyseProduction(root, targetRecursiveRule); // ADDING CHILD TO THE ROOT NODE
                        }
                        else
                        {
                            success = AnalyseProduction(currentNode, targetRecursiveRule);
                        }
                        
                        //success = AnalyseProduction(currentNode, prule[i].RecursiveRule /* always not null */ ?? RuleType.Program);
                        
                        if (success) // IF NON-TERMINAL IS SUCCESSFULLY ANALYSED
                            i++;
                        else
                            break; // NEXT PRODUCTION
                    }
                    else
                    {
                        // IF TERMINAL IS MATCHED
                        if (Regex.IsMatch(lexemes[index].Value, prule[i].Value))
                        {
                            currentNode.Tokens.Add(lexemes[index]);
                            index++;
                            i++;
                        }
                        else
                        {
                            break; // NEXT PRODUCTION
                        }
                    }

                    if (i == prule.Length) // IF WHOLE RULE IS ANALYSED
                    {
                        currentNode.NodeType = ruleType;
                        root.Childrens.Add(currentNode); // ADD AS CHILD OF ROOT NODE
                        return true; // RETURN SUCCESS
                    }
                }
            }

            return false;
        }

        return Result<ASTNode>.Ok(programNode);
    }
}