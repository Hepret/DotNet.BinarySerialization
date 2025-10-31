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
        //context.RegisterPostInitializationOutput(PostInitializationCallback);


        var provider = context.SyntaxProvider.CreateSyntaxProvider
        (
            SyntacticPredicate, 
            SemanticTransform
        );

        context.RegisterSourceOutput(provider, Execute);
    }

    private void Execute(SourceProductionContext arg1, (TypeDeclarationSyntax candidate, ISymbol symbol)? arg2)
    {
        return;
    }



    private static bool SyntacticPredicate(SyntaxNode node, CancellationToken cancellationToken)
    {
        return node is TypeDeclarationSyntax type
               && type.Modifiers.Any(SyntaxKind.PartialKeyword)
               && type.Modifiers.Any(SyntaxKind.PartialKeyword)
               && !type.Modifiers.Any(SyntaxKind.AbstractKeyword)
               && type.BaseList?.Types.Any() is true
               && node is ClassDeclarationSyntax or StructDeclarationSyntax;
    }

    private (TypeDeclarationSyntax candidate, ISymbol symbol)? SemanticTransform(GeneratorSyntaxContext context, CancellationToken cancellationToken)
    {
        
        Debug.Assert(context.Node is TypeDeclarationSyntax);
        var candidate = Unsafe.As<TypeDeclarationSyntax>(context.Node);
        ISymbol? symbol = context.SemanticModel.GetDeclaredSymbol(candidate);
        if (symbol is not INamedTypeSymbol type)
            return null;
        
        var binarySerializableInterface = context.SemanticModel.Compilation.GetTypeByMetadataName("BinarySerialization.Common.IBinarySerializable");
        if (binarySerializableInterface is null || !type.Interfaces.Contains(binarySerializableInterface))
            return null;
        return (candidate, symbol);
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