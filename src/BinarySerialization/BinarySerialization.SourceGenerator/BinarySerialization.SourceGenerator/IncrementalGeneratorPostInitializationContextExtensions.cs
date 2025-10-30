using BinarySerialization.SourceGenerator.GeneratedAttributes;
using Microsoft.CodeAnalysis;

namespace BinarySerialization.SourceGenerator;

public static class IncrementalGeneratorPostInitializationContextExtensions
{
    private const string FileNameTemplate = "Hepret.BinarySerialization.{0}.g.cs";
    public static IncrementalGeneratorPostInitializationContext AddBinarySerializableAttribute(
        this IncrementalGeneratorPostInitializationContext context)
    {
        context.AddSource(string.Format(FileNameTemplate, "SerializableAttribute"), BinarySerializableAttributeDeclaration.Text);
        return context;
    }
    
    public static IncrementalGeneratorPostInitializationContext AddFieldAttribute(
        this IncrementalGeneratorPostInitializationContext context)
    {
        context.AddSource("Hepret.BinaryFieldAttribute.g.cs", BinaryFieldAttributeDeclaration.Text);
        return context;
    }
    
    public static IncrementalGeneratorPostInitializationContext AddEncodingEnum(
        this IncrementalGeneratorPostInitializationContext context)
    {
        context.AddSource("Hepret.BinaryFieldAttribute.g.cs", BinaryFieldAttributeDeclaration.Text);
        return context;
    }

    
}