namespace BinarySerialization.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class FieldAttribute : Attribute 
{ 
    public int Position { get; init; }
    public string DescriptionName { get; init; } = null!;
}