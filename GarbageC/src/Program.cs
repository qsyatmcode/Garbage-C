using System.Data;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using GarbageC.CodeGeneration;

namespace GarbageC;

using Constructs;
using FrontEnd;
using static Result<List<string>>;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: ./.exe <input>");
            return;
        }

        var watch = Stopwatch.StartNew();
        var lex = LexicalParser.Parse(args[0]).GetValueOrThrow();
        var lexElapsed = watch.ElapsedMilliseconds;
        Console.WriteLine($"Lexical analyses takes {lexElapsed}ms\n");
        
        var lexemes = new List<Lexeme>();
        
        foreach (var l in lex)
        {
            var t = DetermineMatchType(l);
            lexemes.Add(CreateLexeme(l, t));
        }
        
        foreach (var pat in lexemes)
        {
            Console.WriteLine(pat);
        }
        
        watch.Restart();
        var d = SyntaxParser.Parse(lexemes).GetValueOrThrow();
        var parseElapsed = watch.ElapsedMilliseconds;
        Console.WriteLine($"AST Parsing takes {parseElapsed}ms\n");
        
        PrintAST(d);
        watch.Restart();
        //Generator.Generate(d, "test");
        var generationElapsed = watch.ElapsedMilliseconds;
        Console.WriteLine($"CIL generation takes {generationElapsed}ms");
        
        watch.Stop();
        Console.WriteLine($"Total elapsed time: {lexElapsed + parseElapsed + generationElapsed}ms\n");
    }

    private static void PrintAST(ASTNode root, int tabs = 0)
    {
        Tabs();
        Console.WriteLine($"Type: {root.NodeType}");
        Tabs();
        Console.WriteLine("Tokens:");

        foreach (var token in root.Tokens)
        {
            Tabs();
            Console.WriteLine($":{token}");
        }
        
        //Tabs();
        //Console.WriteLine("}");
        Tabs();
        Console.WriteLine("Childrens:");

        foreach (var node in root.Childrens)
        {
            PrintAST(node, tabs + 1);
        }

        void Tabs()
        {
            for (int i = 0; i < tabs; i++)
            {
                Console.Write("    ");
            }
        }
    }

    static LexemeType DetermineMatchType(string match) => 
        (from pattern in LexicalParser.Patterns 
            where Regex.Count(match, pattern.Value) != 0 
            orderby (int)pattern.Key 
            select pattern.Key).First();

    static Lexeme CreateLexeme(string value, LexemeType type)
    {
        return new Lexeme(type, value, false, null, false);
    }
}