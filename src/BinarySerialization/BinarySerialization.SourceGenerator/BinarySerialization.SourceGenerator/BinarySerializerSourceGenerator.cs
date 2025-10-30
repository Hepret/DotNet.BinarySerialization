using Microsoft.CodeAnalysis;

namespace BinarySerialization.SourceGenerator;

[Generator(LanguageNames.CSharp)]
public class BinarySerializerSourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(PostInitializationCallback);
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