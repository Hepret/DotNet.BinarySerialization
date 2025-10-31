using BinarySerialization.Common;

namespace BinarySerialization.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class NumFieldAttribute : Attribute
{ 
    public int Position { get; init; }
    public Endian Endian { get; init; } = Endian.Little;
}