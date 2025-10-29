namespace CommonTypes;

/// <summary>
/// Specifies the byte order (endianness) used when representing multi-byte values in memory or during data transmission.
/// </summary>
public enum Endian
{
    /// <summary>
    /// Big-endian — the most significant byte is stored at the lowest memory address (order from most significant to least significant).
    /// </summary>
    Big = 0,

    /// <summary>
    /// Little-endian — the least significant byte is stored at the lowest memory address (order from least significant to most significant).
    /// </summary>
    Little = 1
}
