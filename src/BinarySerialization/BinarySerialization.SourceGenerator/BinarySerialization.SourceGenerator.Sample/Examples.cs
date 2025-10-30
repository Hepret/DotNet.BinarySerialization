using System.Collections.Generic;
using Hepret.BinarySerialization.Attributes;

[BinarySerializable]
class Sample
{
    
    private string MyString { get; set; } = "Hello world";
    
    [Field(Position = 1)]
    private int Num { get; set; }
}