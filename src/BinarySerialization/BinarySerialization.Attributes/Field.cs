namespace BinarySerialization.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class Field : Attribute
{
    public Type? Type { get; set; }
    public int Position { get; set; }
}