using CommonTypes;

namespace BinarySerialization.Attributes;

public class StringField : Field
{
    public Encoding Encoding { get; set; }
    public bool WithBOM { get; set; } = false;
}