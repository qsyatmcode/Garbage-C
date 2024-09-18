using System.Runtime.CompilerServices;
using GarbageC.Constructs;

namespace GarbageC.CodeGeneration;

using Mono.Cecil;
using Mono.Cecil.Cil;
using System.Reflection;
using System.Reflection.Emit;
using Lokad.ILPack;

public class Generator
{
    public static void Generate(ASTNode ast, string assemblyName)
    {
        const string ASSEMBLY_NAME = "IL_Test";

        AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
            new AssemblyName(ASSEMBLY_NAME), AssemblyBuilderAccess.Run);
        
        ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(
            ASSEMBLY_NAME);
        
        TypeBuilder typeBuilder = moduleBuilder.DefineType("Program", 
            System.Reflection.TypeAttributes.Class | System.Reflection.TypeAttributes.Public | System.Reflection.TypeAttributes.BeforeFieldInit);
        //MethodBuilder methodBuilder = typeBuilder.DefineMethod(
        //    "Main", System.Reflection.MethodAttributes.HideBySig | System.Reflection.MethodAttributes.Public | System.Reflection.MethodAttributes.Static,
        //    typeof(Int32), new Type[] { typeof(string[]) });
        
        // Добавляем атрибут MethodImpl(MethodImplOptions.PreserveSig)
        //ConstructorInfo methodImplCtor = typeof(MethodImplAttribute).GetConstructor(new Type[] { typeof(MethodImplOptions) });
        //CustomAttributeBuilder methodImplAttribute = new CustomAttributeBuilder(methodImplCtor, new object[] { MethodImplOptions.PreserveSig });
        //methodBuilder.SetCustomAttribute(methodImplAttribute);

        if (ast.NodeType == RuleType.Program)
        {
            foreach (var function in ast.Childrens) // functions
            {
                ILGenerator generator = Operations.Function(function, typeBuilder) ?? throw new NullReferenceException("Null function");
                EmitAST(function, generator);
            }
        }
        
        //ILGenerator generator = methodBuilder.GetILGenerator();
        
        //generator.EmitWriteLine("Hello World!");
        //generator.Emit(System.Reflection.Emit.OpCodes.Ldc_I4_0);
        //generator.Emit(System.Reflection.Emit.OpCodes.Ret);
        //EmitAST(ast, generator);
        
        typeBuilder.CreateType();
        
        File.Delete("test.dll");
        File.Delete("teste.dll");
        File.Delete("test.exe");
        
        var gen = new Lokad.ILPack.AssemblyGenerator();

        var bytes = gen.GenerateAssemblyBytes(assemblyBuilder);
        
        //Console.WriteLine("ganarate");
        gen.GenerateAssembly(moduleBuilder.Assembly, "test.dll");

        Edit();
    }

    private static void EmitAST(ASTNode node, ILGenerator generator)
    {
        if (node.Childrens.Count != 0)
        {
            foreach (var child in node.Childrens)
            {
                EmitAST(child, generator);
            }
        }

        switch (node.NodeType)
        {
            case RuleType.Expression:
                Operations.Expression(node, generator);
                break;
            case RuleType.Statement:
                Operations.ReturnStatement(generator);
                break;
        }
    }

    private static class Operations
    {
        internal static void Expression(ASTNode node, ILGenerator generator)
        {
            Lexeme literal = node.Tokens.Find(lexem => lexem.Type == LexemeType.IntegerLiteral) ?? throw new Exception("Expression value not found");
            generator.Emit(System.Reflection.Emit.OpCodes.Ldc_I4, int.Parse(literal.Value));
        }

        internal static void ReturnStatement(ILGenerator generator)
        {
            generator.Emit(System.Reflection.Emit.OpCodes.Ret);
        }

        internal static ILGenerator? Function(ASTNode node ,TypeBuilder typeBuilder)
        {
            Lexeme identifier = node.Tokens.Find(lexeme => lexeme.Type == LexemeType.Identifier) ?? throw new Exception("Function identifier not found");
            Lexeme returnType = node.Tokens.ElementAt(0);   //node.Tokens.Find(lexeme => lexeme.Type == LexemType.IntKeyword) ?? throw new Exception("Function return type not found");
            // TODO: argmuments

            ILGenerator? result = null;
            
            if (identifier.Value == "main") // TODO: extract int argc and char* args[]
            {
                Type rt = typeof(void);
                if (returnType.Type == LexemeType.IntKeyword)
                    rt = typeof(Int32);
                
                var methodBuilder = typeBuilder.DefineMethod(
                    "Main", System.Reflection.MethodAttributes.HideBySig | System.Reflection.MethodAttributes.Public | System.Reflection.MethodAttributes.Static,
                    /*typeof(Int32)*/rt, new Type[] { typeof(string[]) });

                result = methodBuilder.GetILGenerator();
            }

            return result;
        }
    }
    
    private static void Edit()
    {
        var readerParameters = new ReaderParameters { ReadWrite = true };
        using (var assemblyDefinition = AssemblyDefinition.ReadAssembly("test.dll", readerParameters))
        {
            // Поиск типа Program (или другого типа, где находится Main)
            var programType = assemblyDefinition.MainModule.Types
                .FirstOrDefault(t => t.Name == "Program");

            if (programType == null)
            {
                Console.WriteLine("Type 'Program' not found in the assembly.");
                return;
            }

            // Поиск метода Main
            var mainMethod = programType.Methods
                .FirstOrDefault(m => m.Name == "Main" && m.IsStatic);

            
            
            if (mainMethod == null)
            {
                Console.WriteLine("Static 'Main' method not found in the 'Program' type.");
                return;
            }

            // Установка точки входа
            assemblyDefinition.EntryPoint = mainMethod;

            // Сохранение измененной сборки
            assemblyDefinition.Write("teste.dll");
        }

        Console.WriteLine($"Entry point set successfully. Modified assembly saved to {"teste.dll"}");
    }
}