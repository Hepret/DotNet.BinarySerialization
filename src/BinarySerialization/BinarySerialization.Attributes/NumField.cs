using CommonTypes;

namespace BinarySerialization.Attributes;

public class NumField : Field
{
    public Endian Endian { get; set; } = Endian.Little;
}