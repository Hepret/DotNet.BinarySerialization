using System.Text;
using CommonTypes;

namespace BinaryData.Extensions;

/// <summary>
/// A set of extension methods for appending numeric and string values to <see cref="BinaryData"/>
/// with support for specifying byte order (endianness).
/// </summary>
public static class BinaryDataExtensions
{
    #region Append Methods

    #region Int32

    /// <inheritdoc cref="AddInt32"/>
    public static BinaryData AddInteger(this BinaryData binary, int value, Endian endian = Endian.Little)
        => AddInt32(binary, value, endian);

    /// <summary>
    /// Appends a 32-bit signed integer to the binary data.
    /// </summary>
    /// <param name="binary">The target <see cref="BinaryData"/> instance.</param>
    /// <param name="value">The value to append.</param>
    /// <param name="endian">Byte order (defaults to Little-endian).</param>
    /// <returns>A reference to the same <paramref name="binary"/> object for method chaining.</returns>
    public static BinaryData AddInt32(this BinaryData binary, int value, Endian endian = Endian.Little)
    {
        var bytes = BitConverter.GetBytes(value);
        ReverseIfNeed(bytes, endian);
        binary.AddRange(bytes);
        return binary;
    }

    #endregion

    #region Uint32

    /// <inheritdoc cref="AddUint32"/>
    public static BinaryData AddUint(this BinaryData binary, uint value, Endian endian = Endian.Little)
        => AddUint32(binary, value, endian);

    /// <summary>
    /// Appends a 32-bit unsigned integer to the binary data.
    /// </summary>
    /// <param name="binary">The target <see cref="BinaryData"/> instance.</param>
    /// <param name="value">The value to append.</param>
    /// <param name="endian">Byte order (defaults to Little-endian).</param>
    /// <returns>A reference to the same <paramref name="binary"/> object for method chaining.</returns>
    public static BinaryData AddUint32(this BinaryData binary, uint value, Endian endian = Endian.Little)
    {
        var bytes = BitConverter.GetBytes(value);
        ReverseIfNeed(bytes, endian);
        binary.AddRange(bytes);
        return binary;
    }

    #endregion

    #region Int16

    /// <inheritdoc cref="AddInt16"/>
    public static BinaryData AddShort(this BinaryData binary, short value, Endian endian = Endian.Little)
        => AddInt16(binary, value, endian);

    /// <summary>
    /// Appends a 16-bit signed integer to the binary data.
    /// </summary>
    /// <param name="binary">The target <see cref="BinaryData"/> instance.</param>
    /// <param name="value">The value to append.</param>
    /// <param name="endian">Byte order (defaults to Little-endian).</param>
    /// <returns>A reference to the same <paramref name="binary"/> object for method chaining.</returns>
    public static BinaryData AddInt16(this BinaryData binary, short value, Endian endian = Endian.Little)
    {
        var bytes = BitConverter.GetBytes(value);
        ReverseIfNeed(bytes, endian);
        binary.AddRange(bytes);
        return binary;
    }

    #endregion

    #region Uint16

    /// <inheritdoc cref="AddUint16"/>
    public static BinaryData AddUshort(this BinaryData binary, ushort value, Endian endian = Endian.Little)
        => AddUint16(binary, value, endian);

    /// <summary>
    /// Appends a 16-bit unsigned integer to the binary data.
    /// </summary>
    /// <param name="binary">The target <see cref="BinaryData"/> instance.</param>
    /// <param name="value">The value to append.</param>
    /// <param name="endian">Byte order (defaults to Little-endian).</param>
    /// <returns>A reference to the same <paramref name="binary"/> object for method chaining.</returns>
    public static BinaryData AddUint16(this BinaryData binary, ushort value, Endian endian = Endian.Little)
    {
        var bytes = BitConverter.GetBytes(value);
        ReverseIfNeed(bytes, endian);
        binary.AddRange(bytes);
        return binary;
    }

    #endregion

    #region Int64

    /// <inheritdoc cref="AddInt64"/>
    public static BinaryData AddLong(this BinaryData binary, long value, Endian endian = Endian.Little)
        => AddInt64(binary, value, endian);

    /// <summary>
    /// Appends a 64-bit signed integer to the binary data.
    /// </summary>
    /// <param name="binary">The target <see cref="BinaryData"/> instance.</param>
    /// <param name="value">The value to append.</param>
    /// <param name="endian">Byte order (defaults to Little-endian).</param>
    /// <returns>A reference to the same <paramref name="binary"/> object for method chaining.</returns>
    public static BinaryData AddInt64(this BinaryData binary, long value, Endian endian = Endian.Little)
    {
        var bytes = BitConverter.GetBytes(value);
        ReverseIfNeed(bytes, endian);
        binary.AddRange(bytes);
        return binary;
    }

    #endregion

    #region Uint64

    /// <inheritdoc cref="AddUint64"/>
    public static BinaryData AddUlong(this BinaryData binary, ulong value, Endian endian = Endian.Little)
        => AddUint64(binary, value, endian);

    /// <summary>
    /// Appends a 64-bit unsigned integer to the binary data.
    /// </summary>
    /// <param name="binary">The target <see cref="BinaryData"/> instance.</param>
    /// <param name="value">The value to append.</param>
    /// <param name="endian">Byte order (defaults to Little-endian).</param>
    /// <returns>A reference to the same <paramref name="binary"/> object for method chaining.</returns>
    public static BinaryData AddUint64(this BinaryData binary, ulong value, Endian endian = Endian.Little)
    {
        var bytes = BitConverter.GetBytes(value);
        ReverseIfNeed(bytes, endian);
        binary.AddRange(bytes);
        return binary;
    }

    #endregion

    #region Float

    /// <summary>
    /// Appends a 32-bit floating-point number to the binary data.
    /// </summary>
    /// <param name="binary">The target <see cref="BinaryData"/> instance.</param>
    /// <param name="value">The value to append.</param>
    /// <param name="endian">Byte order (defaults to Little-endian).</param>
    /// <returns>A reference to the same <paramref name="binary"/> object for method chaining.</returns>
    public static BinaryData AddFloat(this BinaryData binary, float value, Endian endian = Endian.Little)
    {
        var bytes = BitConverter.GetBytes(value);
        ReverseIfNeed(bytes, endian);
        binary.AddRange(bytes);
        return binary;
    }

    #endregion

    #region Double

    /// <summary>
    /// Appends a 64-bit floating-point number to the binary data.
    /// </summary>
    /// <param name="binary">The target <see cref="BinaryData"/> instance.</param>
    /// <param name="value">The value to append.</param>
    /// <param name="endian">Byte order (defaults to Little-endian).</param>
    /// <returns>A reference to the same <paramref name="binary"/> object for method chaining.</returns>
    public static BinaryData AddDouble(this BinaryData binary, double value, Endian endian = Endian.Little)
    {
        var bytes = BitConverter.GetBytes(value);
        ReverseIfNeed(bytes, endian);
        binary.AddRange(bytes);
        return binary;
    }

    #endregion

    #region Char

    /// <summary>
    /// Appends a Unicode character (16-bit) to the binary data.
    /// </summary>
    /// <param name="binary">The target <see cref="BinaryData"/> instance.</param>
    /// <param name="value">The character to append.</param>
    /// <returns>A reference to the same <paramref name="binary"/> object for method chaining.</returns>
    public static BinaryData AddChar(this BinaryData binary, char value)
    {
        var bytes = BitConverter.GetBytes(value);
        binary.AddRange(bytes);
        return binary;
    }

    #endregion

    #region String

    /// <summary>
    /// Appends a string with its Byte Order Mark (BOM) using the specified encoding.
    /// </summary>
    /// <param name="binary">The target <see cref="BinaryData"/> instance.</param>
    /// <param name="value">The string to append.</param>
    /// <param name="encoding">The encoding to use. Must support BOM (e.g., UTF-8, UTF-16, UTF-32).</param>
    /// <returns>A reference to the same <paramref name="binary"/> object.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> or <paramref name="encoding"/> is <c>null</c>.</exception>
    public static BinaryData AddStringWithBom(this BinaryData binary, string value, Encoding encoding)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        if (encoding is null)
            throw new ArgumentNullException(nameof(encoding));

        // Get the BOM (preamble) for the encoding, if supported
        var preamble = encoding.GetPreamble();
        var content = encoding.GetBytes(value);

        binary.AddRange(preamble);
        binary.AddRange(content);
        return binary;
    }

    /// <summary>
    /// Appends a string encoded as UTF-8 (without BOM).
    /// </summary>
    /// <param name="binary">The target <see cref="BinaryData"/> instance.</param>
    /// <param name="value">The string to append.</param>
    /// <returns>A reference to the same <paramref name="binary"/> object.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <c>null</c>.</exception>
    public static BinaryData AddUtf8String(this BinaryData binary, string value)
    {
        if (value is null) throw new ArgumentNullException(nameof(value));
        var bytes = Encoding.UTF8.GetBytes(value);
        binary.AddRange(bytes);
        return binary;
    }

    /// <summary>
    /// Appends a string encoded as UTF-16 (Unicode, without BOM).
    /// </summary>
    /// <param name="binary">The target <see cref="BinaryData"/> instance.</param>
    /// <param name="value">The string to append.</param>
    /// <returns>A reference to the same <paramref name="binary"/> object.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <c>null</c>.</exception>
    public static BinaryData AddUnicodeString(this BinaryData binary, string value)
    {
        if (value is null) throw new ArgumentNullException(nameof(value));
        var bytes = Encoding.Unicode.GetBytes(value);
        binary.AddRange(bytes);
        return binary;
    }

    /// <summary>
    /// Appends a string encoded as UTF-32 (without BOM).
    /// </summary>
    /// <param name="binary">The target <see cref="BinaryData"/> instance.</param>
    /// <param name="value">The string to append.</param>
    /// <returns>A reference to the same <paramref name="binary"/> object.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <c>null</c>.</exception>
    public static BinaryData AddUtf32String(this BinaryData binary, string value)
    {
        if (value is null) throw new ArgumentNullException(nameof(value));
        var bytes = Encoding.UTF32.GetBytes(value);
        binary.AddRange(bytes);
        return binary;
    }

    /// <summary>
    /// Appends a string encoded as ASCII.
    /// </summary>
    /// <param name="binary">The target <see cref="BinaryData"/> instance.</param>
    /// <param name="value">The string to append.</param>
    /// <returns>A reference to the same <paramref name="binary"/> object.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <c>null</c>.</exception>
    public static BinaryData AddAsciiString(this BinaryData binary, string value)
    {
        if (value is null) throw new ArgumentNullException(nameof(value));
        var bytes = Encoding.ASCII.GetBytes(value);
        binary.AddRange(bytes);
        return binary;
    }

    #endregion

    #endregion

    #region Helper Methods

    private static void ReverseIfNeed(byte[] bytes, Endian endian)
    {
        var needReverse = CheckNeedReverse(endian);
        if (needReverse)
            Array.Reverse(bytes);
    }

    private static bool CheckNeedReverse(Endian endian)
    {
        var bitConverterIsLittleEndian = BitConverter.IsLittleEndian;
        switch (bitConverterIsLittleEndian)
        {
            case true when endian == Endian.Little:
            case false when endian == Endian.Big:
                return false;
            case true when endian == Endian.Big:
            case false when endian == Endian.Little:
                return true;
            default:
                throw new InvalidOperationException();
        }
    }

    #endregion
}