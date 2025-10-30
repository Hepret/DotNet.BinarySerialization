using BinarySerialization.SourceGenerator.CommonGeneratedTypes;
using BinarySerialization.SourceGenerator.GeneratedAttributes;
using Microsoft.CodeAnalysis;

namespace BinarySerialization.SourceGenerator;

public static class IncrementalGeneratorPostInitializationContextExtensions
{
    private const string FileBaseNameTemplate = "Hepret.BinarySerialization.{0}.g.cs";
    private static readonly string AttributesFileNameTemplate = string.Format(FileBaseNameTemplate, "Attributes.{0}");
    private static readonly string CommonFileNameTemplate = string.Format(FileBaseNameTemplate, "Common.{0}");
    
    public static IncrementalGeneratorPostInitializationContext AddBinarySerializableAttribute(
        this IncrementalGeneratorPostInitializationContext context)
    {
        context.AddSource(GenerateAttributeFileName("SerializableAttribute"), BinarySerializableAttributeDeclaration.Text);
        return context;
    }
    
    public static IncrementalGeneratorPostInitializationContext AddFieldAttribute(
        this IncrementalGeneratorPostInitializationContext context)
    {
        context.AddSource(GenerateAttributeFileName("BinaryFieldAttribute"), BinaryFieldAttributeDeclaration.Text);
        return context;
    }
    
    public static IncrementalGeneratorPostInitializationContext AddNumFieldAttribute(
        this IncrementalGeneratorPostInitializationContext context)
    {
        context.AddSource(GenerateAttributeFileName("BinaryNumFieldAttribute"), BinaryNumFieldAttributeDeclaration.Text);
        return context;
    }
    
    public static IncrementalGeneratorPostInitializationContext AddEncodingEnum(
        this IncrementalGeneratorPostInitializationContext context)
    {
        context.AddSource(GenerateCommonFileName("Encoding"), EnumEncodingDeclaration.Text);
        return context;
    }
    
    public static IncrementalGeneratorPostInitializationContext AddEndianEnum(
        this IncrementalGeneratorPostInitializationContext context)
    {
        context.AddSource(GenerateCommonFileName("Endian"), EnumEndianDeclaration.Text);
        return context;
    }
    
    public static IncrementalGeneratorPostInitializationContext AddIBinarySerializableInterface(
        this IncrementalGeneratorPostInitializationContext context)
    {
        context.AddSource(GenerateCommonFileName("IBinarySerializable"), InterfaceIBinarySerializableDeclaration.Text);
        return context;
    }


    private static string GenerateCommonFileName(string name)
    {
        return string.Format(CommonFileNameTemplate, name);
    }
    private static string GenerateAttributeFileName(string name)
    {
        return string.Format(AttributesFileNameTemplate, name);
    }
}