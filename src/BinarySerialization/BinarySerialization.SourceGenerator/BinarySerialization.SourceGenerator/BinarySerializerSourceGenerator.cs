using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BinarySerialization.SourceGenerator;

struct BinarySerializationGenerationContext
{
    
}


[Generator(LanguageNames.CSharp)]
public class BinarySerializerSourceGenerator : IIncrementalGenerator
{
    
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Add Generated Types
        context.RegisterPostInitializationOutput(PostInitializationCallback);

      
        var provider = context.SyntaxProvider.CreateSyntaxProvider
        (
            SyntacticPredicate, 
            SemanticTransform
        );

        context.RegisterSourceOutput(provider, Execute);
    }

    private void Execute(SourceProductionContext context, BinarySerializationGenerationContext? subject)
    {
        if (!subject.HasValue)
            return;
        var num = 0;
        num += 1;
    }


    private static bool SyntacticPredicate(SyntaxNode node, CancellationToken cancellationToken)
    {
        return node is ClassDeclarationSyntax or StructDeclarationSyntax
               && ((TypeDeclarationSyntax)node).Modifiers.Any(SyntaxKind.PartialKeyword)
               && !((TypeDeclarationSyntax)node).Modifiers.Any(SyntaxKind.StaticKeyword)
               && ((TypeDeclarationSyntax)node).BaseList is not null;
    }

    private BinarySerializationGenerationContext? SemanticTransform(GeneratorSyntaxContext context, CancellationToken cancellationToken)
    {
        
        Debug.Assert(context.Node is TypeDeclarationSyntax);
        var candidate = Unsafe.As<TypeDeclarationSyntax>(context.Node);
        ISymbol? symbol = context.SemanticModel.GetDeclaredSymbol(candidate);
        if (symbol is not INamedTypeSymbol type)
            return null;
        
        var binarySerializableInterface = context.SemanticModel.Compilation.GetTypeByMetadataName("Hepret.BinarySerialization.Common.IBinarySerializable");
        if (binarySerializableInterface is null || !type.Interfaces.Contains(binarySerializableInterface))
            return null;
        var binaryContext = GetBinarySerializationContext(context, candidate, type);
        return binaryContext;
    }

    private BinarySerializationGenerationContext? GetBinarySerializationContext(
        GeneratorSyntaxContext context,
        TypeDeclarationSyntax typeDeclaration,
        INamedTypeSymbol type)
    {
        var members = typeDeclaration.Members
            .Where(x => x is PropertyDeclarationSyntax or FieldDeclarationSyntax);
        return null;
    }

    private static void PostInitializationCallback(IncrementalGeneratorPostInitializationContext context)
    {
        context
            .AddBinarySerializableAttribute()
            .AddFieldAttribute()
            .AddEncodingEnum()
            .AddEndianEnum()
            .AddNumFieldAttribute()
            .AddIBinarySerializableInterface();
    }
}