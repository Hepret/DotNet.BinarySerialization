namespace BinarySerialization.SourceGenerator.Tests;

using System.Collections.Immutable;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using BinarySerialization.SourceGenerator.Tests.Utils;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;



public class BinarySerializationSourceGeneratorTest
{
 
    [Fact]
    public void ShouldGeneratePartial()
    {
        // Create an instance of the source generator.
        var generator = new BinarySerializerSourceGenerator();

        // Source generators should be tested using 'GeneratorDriver'.
        var driver = CSharpGeneratorDriver.Create(generator);
        
        
    }
}