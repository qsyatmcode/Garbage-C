using System.Reflection;
using System.Reflection.Emit;
using Antlr4.Runtime.Tree;
using GarbageC.FrontEnd;
using Mono.Cecil;

namespace GarbageC.BackEnd;

public class Generator
{
    private static Dictionary<string, ILGenerator> _functions = new();
    
    public static ILEmitter Emitter = new ILEmitter();
    
    private static string _sourceFileName = String.Empty;
    private static string _finalFileName = String.Empty;
    private static string _ROFileName = String.Empty; // Runtime options file name
    private static TypeBuilder _typeBuilder;

    private static readonly string _runtimeConfig =
@"{
    ""runtimeOptions"": {
        ""tfm"": ""net8.0"",
        ""framework"": {
            ""name"": ""Microsoft.NETCore.App"",
            ""version"": ""8.0.0""
        },
        ""configProperties"": {
            ""System.Runtime.Serialization.EnableUnsafeBinaryFormatterSerialization"": false
        }
    }
}";

    public class ILEmitter
    {
        public void Return()
        {
            ILGenerator generator = _functions.Last().Value;
            generator.Emit(OpCodes.Ret);
        }

        public void Expression(int evalValue)
        {
            ILGenerator generator = _functions.Last().Value;
            generator.Emit(OpCodes.Ldc_I4, evalValue);
        }
    }
    
    public static void Generate(ParseTreeWalker walker, cprogramParser.ProgramContext tree,  string assemblyName)
    {
        AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
            new AssemblyName(assemblyName), AssemblyBuilderAccess.Run);
        
        ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(
            assemblyName);
        
        _typeBuilder = moduleBuilder.DefineType("Program", 
            System.Reflection.TypeAttributes.Class | System.Reflection.TypeAttributes.Public | System.Reflection.TypeAttributes.BeforeFieldInit);
        
        walker.Walk(new ProgramListener( new ProgramVisitor() ), tree);
        
        _typeBuilder.CreateType();

        _ROFileName = $"{assemblyName}.runtimeconfig.json";
        _sourceFileName = $"{assemblyName}_src.dll"; // before editing
        _finalFileName = $"{assemblyName}.dll"; // after editing
        
        File.Delete(_sourceFileName);
        File.Delete(_finalFileName);
        
        
        var gen = new Lokad.ILPack.AssemblyGenerator();
        
        gen.GenerateAssembly(moduleBuilder.Assembly, _sourceFileName);

        Edit();
        
        CreateRuntimeOptions();
    }

    public static void AddFunction(string id)
    {
        //Type rt = typeof(void);
        //if (returnType.Type == LexemeType.IntKeyword)
        //    rt = typeof(Int32);
        if (id == "main")
        {
            var methodBuilder = _typeBuilder.DefineMethod(
                "Main", System.Reflection.MethodAttributes.HideBySig | System.Reflection.MethodAttributes.Public | System.Reflection.MethodAttributes.Static,
                typeof(Int32), new Type[] { typeof(string[]) });
            _functions.Add(id, methodBuilder.GetILGenerator());
        }
        else
        {
            //Functions.Add(id, methodBuilder.GetILGenerator());
        }
    }
    
    private static void Edit()
    {
        var readerParameters = new ReaderParameters { ReadWrite = true };
        using (var assemblyDefinition = AssemblyDefinition.ReadAssembly(_sourceFileName, readerParameters))
        {
            var programType = assemblyDefinition.MainModule.Types
                .FirstOrDefault(t => t.Name == "Program");

            if (programType == null)
            {
                Console.WriteLine("Type 'Program' not found in the assembly.");
                return;
            }

            var mainMethod = programType.Methods
                .FirstOrDefault(m => m.Name == "Main" && m.IsStatic);
            
            if (mainMethod == null)
            {
                Console.WriteLine("Static 'Main' method not found in the 'Program' type.");
                return;
            }

            assemblyDefinition.EntryPoint = mainMethod;
            
            assemblyDefinition.Write(_finalFileName); // save final assembly
        }

        Console.WriteLine($"Entry point set successfully. Final assembly version saved to {_finalFileName}");
    }

    private static void CreateRuntimeOptions()
    {
        if (!File.Exists(_ROFileName))
        {
            using (StreamWriter writer = File.CreateText(_ROFileName))
            {
                writer.Write(_runtimeConfig);
                writer.Flush();
            }
            Console.WriteLine($"Runtime options written in {_ROFileName}");
        }
    }
}