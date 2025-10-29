using System;
using System.Linq;
using System.Text;
using BinaryData.Extensions;
using CommonTypes;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;
using Encoding = System.Text.Encoding;

namespace BinaryData.Tests;

[TestSubject(typeof(BinaryDataExtensions))]
public class BinaryDataExtensionsTest
{
     [Theory]
    [InlineData(0x12345678, Endian.Little, new byte[] { 0x78, 0x56, 0x34, 0x12 })]
    [InlineData(0x12345678, Endian.Big,    new byte[] { 0x12, 0x34, 0x56, 0x78 })]
    public void AddInt32_ShouldAddBytesInCorrectOrder(int value, Endian endian, byte[] expected)
    {
        // Act
        var data = new BinaryData().AddInt32(value, endian);

        // Assert
        data.Should().Equal(expected);
    }

    [Theory]
    [InlineData(0x12345678u, Endian.Little, new byte[] { 0x78, 0x56, 0x34, 0x12 })]
    [InlineData(0x12345678u, Endian.Big,    new byte[] { 0x12, 0x34, 0x56, 0x78 })]
    public void AddUint32_ShouldAddBytesInCorrectOrder(uint value, Endian endian, byte[] expected)
    {
        var data = new BinaryData().AddUint32(value, endian);
        data.Should().Equal(expected);
    }

    [Theory]
    [InlineData(0x1234, Endian.Little, new byte[] { 0x34, 0x12 })]
    [InlineData(0x1234, Endian.Big,    new byte[] { 0x12, 0x34 })]
    public void AddInt16_ShouldAddBytesInCorrectOrder(short value, Endian endian, byte[] expected)
    {
        var data = new BinaryData().AddInt16(value, endian);
        data.Should().Equal(expected);
    }

    [Theory]
    [InlineData(0x1234u, Endian.Little, new byte[] { 0x34, 0x12 })]
    [InlineData(0x1234u, Endian.Big,    new byte[] { 0x12, 0x34 })]
    public void AddUint16_ShouldAddBytesInCorrectOrder(ushort value, Endian endian, byte[] expected)
    {
        var data = new BinaryData().AddUint16(value, endian);
        data.Should().Equal(expected);
    }

    [Theory]
    [InlineData(0x123456789ABCDEF0L, Endian.Little, new byte[] { 0xF0, 0xDE, 0xBC, 0x9A, 0x78, 0x56, 0x34, 0x12 })]
    [InlineData(0x123456789ABCDEF0L, Endian.Big,    new byte[] { 0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE, 0xF0 })]
    public void AddInt64_ShouldAddBytesInCorrectOrder(long value, Endian endian, byte[] expected)
    {
        var data = new BinaryData().AddInt64(value, endian);
        data.Should().Equal(expected);
    }

    [Theory]
    [InlineData(0x123456789ABCDEF0UL, Endian.Little, new byte[] { 0xF0, 0xDE, 0xBC, 0x9A, 0x78, 0x56, 0x34, 0x12 })]
    [InlineData(0x123456789ABCDEF0UL, Endian.Big,    new byte[] { 0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE, 0xF0 })]
    public void AddUint64_ShouldAddBytesInCorrectOrder(ulong value, Endian endian, byte[] expected)
    {
        var data = new BinaryData().AddUint64(value, endian);
        data.Should().Equal(expected);
    }

    [Fact]
    public void AddFloat_ShouldPreserveValue()
    {
        // Arrange
        const float value = 123.456f;

        // Act
        var data = new BinaryData().AddFloat(value);
        var bytes = data.ToArray();
        var restored = BitConverter.ToSingle(bytes, 0);

        // Assert
        restored.Should().Be(value);
    }

    [Fact]
    public void AddDouble_ShouldPreserveValue()
    {
        const double value = 123.456789;
        var data = new BinaryData().AddDouble(value);
        var restored = BitConverter.ToDouble(data.ToArray(), 0);
        restored.Should().Be(value);
    }

    [Fact]
    public void AddChar_ShouldAddUtf16Bytes()
    {
        const char c = 'A';
        var data = new BinaryData().AddChar(c);
        var expected = BitConverter.GetBytes(c);
        data.Should().Equal(expected);
    }

    [Fact]
    public void AddUtf8String_ShouldEncodeCorrectly()
    {
        const string text = "Привет";
        var data = new BinaryData().AddUtf8String(text);
        var expected = Encoding.UTF8.GetBytes(text);
        data.Should().Equal(expected);
    }

    [Fact]
    public void AddUnicodeString_ShouldEncodeAsUtf16Le()
    {
        const string text = "Hello";
        var data = new BinaryData().AddUnicodeString(text);
        var expected = Encoding.Unicode.GetBytes(text);
        data.Should().Equal(expected);
    }

    [Fact]
    public void AddUtf32String_ShouldEncodeCorrectly()
    {
        const string text = "мир";
        var data = new BinaryData().AddUtf32String(text);
        var expected = Encoding.UTF32.GetBytes(text);
        data.Should().Equal(expected);
    }

    [Fact]
    public void AddAsciiString_ShouldEncodeCorrectly()
    {
        const string text = "ABC";
        var data = new BinaryData().AddAsciiString(text);
        var expected = Encoding.ASCII.GetBytes(text);
        data.Should().Equal(expected);
    }

    [Fact]
    public void AddStringWithBom_ShouldPrependPreamble()
    {
        const string text = "Test";
        var encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true);
        var data = new BinaryData().AddStringWithBom(text, encoding);

        var preamble = encoding.GetPreamble();
        var content = encoding.GetBytes(text);
        var expected = new byte[preamble.Length + content.Length];
        Array.Copy(preamble, expected, preamble.Length);
        Array.Copy(content, 0, expected, preamble.Length, content.Length);

        data.Should().Equal(expected);
    }

    [Fact]
    public void AddStringWithBom_WithNullValue_ShouldThrow()
    {
        var data = new BinaryData();
        Action act = () => data.AddStringWithBom(null!, Encoding.UTF8);
        act.Should().Throw<ArgumentNullException>().WithParameterName("value");
    }

    [Fact]
    public void AddStringWithBom_WithNullEncoding_ShouldThrow()
    {
        var data = new BinaryData();
        Action act = () => data.AddStringWithBom("test", null!);
        act.Should().Throw<ArgumentNullException>().WithParameterName("encoding");
    }

    [Fact]
    public void StringMethods_WithNullValue_ShouldThrow()
    {
        var data = new BinaryData();
        Action act1 = () => data.AddUtf8String(null!);
        Action act2 = () => data.AddUnicodeString(null!);
        Action act3 = () => data.AddUtf32String(null!);
        Action act4 = () => data.AddAsciiString(null!);

        act1.Should().Throw<ArgumentNullException>();
        act2.Should().Throw<ArgumentNullException>();
        act3.Should().Throw<ArgumentNullException>();
        act4.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void AddInt32_ShouldReturnSameInstance()
    {
        var data = new BinaryData();
        var result = data.AddInt32(42);
        result.Should().BeSameAs(data);
    }

    [Fact]
    public void AddUtf8String_ShouldReturnSameInstance()
    {
        var data = new BinaryData();
        var result = data.AddUtf8String("test");
        result.Should().BeSameAs(data);
    }

    [Fact]
    public void AddInteger_ShouldWorkAsAddInt32()
    {
        var data1 = new BinaryData().AddInteger(0x12345678, Endian.Big);
        var data2 = new BinaryData().AddInt32(0x12345678, Endian.Big);
        data1.Should().Equal(data2);
    }

    [Fact]
    public void AddShort_ShouldWorkAsAddInt16()
    {
        var data1 = new BinaryData().AddShort(0x1234);
        var data2 = new BinaryData().AddInt16(0x1234);
        data1.Should().Equal(data2);
    }

    [Fact]
    public void AddUshort_ShouldWorkAsAddUint16()
    {
        var data1 = new BinaryData().AddUshort(0xABCD, Endian.Big);
        var data2 = new BinaryData().AddUint16(0xABCD, Endian.Big);
        data1.Should().Equal(data2);
    }

    [Fact]
    public void AddLong_ShouldWorkAsAddInt64()
    {
        var data1 = new BinaryData().AddLong(0x123456789ABCDEF0L);
        var data2 = new BinaryData().AddInt64(0x123456789ABCDEF0L);
        data1.Should().Equal(data2);
    }

    [Fact]
    public void AddUlong_ShouldWorkAsAddUint64()
    {
        var data1 = new BinaryData().AddUlong(0x123456789ABCDEF0UL, Endian.Big);
        var data2 = new BinaryData().AddUint64(0x123456789ABCDEF0UL, Endian.Big);
        data1.Should().Equal(data2);
    }

}