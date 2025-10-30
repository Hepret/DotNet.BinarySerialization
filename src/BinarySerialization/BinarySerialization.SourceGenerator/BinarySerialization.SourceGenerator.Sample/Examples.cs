
using System;
using Hepret.BinarySerialization.Attributes;
using Hepret.BinarySerialization.Common;

namespace Test;

partial struct Sample : IBinarySerializable
{
    public Sample()
    {
        Num = 0;
        Endian = Endian.Little;
    }

    private string MyString { get; set; } = "Hello world";

    public Type TType => typeof(IBinarySerializable);
    
    [NumField(Position = 1, Endian = Endian.Big)]
    private int Num { get; set; }
    
    private Endian Endian { get; set; }
    public byte[] ToBytes()
    {
        throw new System.NotImplementedException();
    }

    public string AsHexString(char separator = '_')
    {
        throw new System.NotImplementedException();
    }

    public string TableInfo()
    {
        throw new System.NotImplementedException();
    }
}



partial class Packet : IBinarySerializable
{
    
}

partial class Packet : IBinarySerializable
{
    public byte[] ToBytes()
    {
        throw new System.NotImplementedException();
    }

    public string AsHexString(char separator = '_')
    {
        throw new System.NotImplementedException();
    }

    public string TableInfo()
    {
        throw new System.NotImplementedException();
    }
}