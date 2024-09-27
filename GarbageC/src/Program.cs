using System.Data;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using GarbageC.BackEnd;
using Antlr4.Runtime;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace GarbageC;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: ./.exe <input>");
            return;
        }

        string source = File.ReadAllText(args[0]);
        AntlrInputStream inputStream = new AntlrInputStream(source); // CharStream
        
        var watch = Stopwatch.StartNew();
        
        cprogramLexer lexer = new cprogramLexer(inputStream);
        CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
        
        var lexElapsed = watch.ElapsedMilliseconds;
        Console.WriteLine($"Lexical analyses takes {lexElapsed}ms\n");
        
        watch.Restart();
        
        cprogramParser parser = new cprogramParser(commonTokenStream);
        cprogramParser.ProgramContext tree = parser.program(); // program is root rule
        
        var parseElapsed = watch.ElapsedMilliseconds;
        Console.WriteLine($"AST Parsing takes {parseElapsed}ms\n");
        
        watch.Restart();
        
        ParseTreeWalker walker = new ParseTreeWalker();
        Generator.Generate(walker, tree, "test");
        
        var generationElapsed = watch.ElapsedMilliseconds;
        Console.WriteLine($"CIL generation takes {generationElapsed}ms");
        
        watch.Stop();
        Console.WriteLine($"Total elapsed time: {lexElapsed + parseElapsed + generationElapsed}ms\n");
    }
}