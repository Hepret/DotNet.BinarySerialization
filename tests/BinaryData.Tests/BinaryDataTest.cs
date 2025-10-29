namespace BinaryData.Tests;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;


[TestSubject(typeof(BinaryData))]
public class BinaryDataTests
{

     [Fact]
    public void Ctor_Default_ShouldCreateEmptyInstance()
    {
        // Arrange & Act
        var data = new BinaryData();

        // Assert
        data.Count.Should().Be(0);
        data.IsReadOnly.Should().BeFalse();
    }

    [Fact]
    public void Ctor_WithBytes_ShouldCopyInput()
    {
        // Arrange
        var input = new byte[] { 1, 2, 3, 4 };

        // Act
        var data = new BinaryData(input);

        // Assert
        data.Should().BeEquivalentTo(input);
        data.Count.Should().Be(4);
    }

    [Fact]
    public void Ctor_WithNullBytes_ShouldThrowArgumentNullException()
    {
        // Act
        Action act = () => new BinaryData(null);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("bytes");
    }

    [Fact]
    public void Add_ShouldAppendByte()
    {
        // Arrange
        var data = new BinaryData();

        // Act
        data.Add(0xFF);

        // Assert
        data.Should().Equal(0xFF);
    }

    [Fact]
    public void AddRange_WithValidBytes_ShouldAppendAll()
    {
        // Arrange
        var data = new BinaryData(new byte[] { 1, 2 });
        var toAdd = new byte[] { 3, 4 };

        // Act
        data.AddRange(toAdd);

        // Assert
        data.Should().Equal(1, 2, 3, 4);
    }

    [Fact]
    public void AddRange_WithNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var data = new BinaryData();

        // Act
        Action act = () => data.AddRange(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("bytes");
    }

    [Fact]
    public void InsertRange_ShouldInsertAtSpecifiedIndex()
    {
        // Arrange
        var data = new BinaryData(new byte[] { 1, 4 });
        var toInsert = new byte[] { 2, 3 };

        // Act
        data.InsertRange(1, toInsert);

        // Assert
        data.Should().Equal(1, 2, 3, 4);
    }

    [Fact]
    public void InsertRange_WithIndexOutOfRange_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var data = new BinaryData(new byte[] { 1, 2 });

        // Act
        Action act = () => data.InsertRange(5, new byte[] { 3 });

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("index");
    }

    [Fact]
    public void InsertRange_WithNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var data = new BinaryData();

        // Act
        Action act = () => data.InsertRange(0, null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("collection");
    }

    [Fact]
    public void Indexer_Range_ShouldReturnNewBinaryDataWithSlice()
    {
        // Arrange
        var data = new BinaryData(new byte[] { 10, 20, 30, 40, 50 });

        // Act
        var slice = data[1..4]; // 20, 30, 40

        // Assert
        slice.Should().BeEquivalentTo(new byte[] { 20, 30, 40 });
        slice.Should().NotBeSameAs(data); // должен быть новый экземпляр
    }

    [Fact]
    public void Indexer_Range_OutOfBounds_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var data = new BinaryData(new byte[] { 1, 2, 3 });

        // Act
        Action act = () => _ = data[..10];

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Stream_ShouldReturnReadOnlyMemoryStreamWithCopyOfData()
    {
        // Arrange
        var originalBytes = new byte[] { 1, 2, 3, 4 };
        var data = new BinaryData(originalBytes);

        // Act
        using var stream = data.Stream;

        // Assert
        stream.Should().BeOfType<MemoryStream>();
        stream.CanRead.Should().BeTrue();
        stream.CanWrite.Should().BeFalse(); // важно: поток только для чтения

        var buffer = new byte[originalBytes.Length];
        var read = stream.Read(buffer, 0, buffer.Length);
        read.Should().Be(originalBytes.Length);
        buffer.Should().Equal(originalBytes);

        // Убедимся, что это копия: изменение оригинала не влияет на поток
        data.Clear();
        stream.Position = 0;
        var buffer2 = new byte[originalBytes.Length];
        stream.ReadExactly(buffer2, 0, buffer2.Length);
        buffer2.Should().Equal(originalBytes); // данные в потоке не изменились
    }

    [Fact]
    public void IListImplementation_ShouldWorkCorrectly()
    {
        // Arrange
        var data = new BinaryData();

        // Act
        data.Add(10);
        data.Insert(0, 5);
        var index = data.IndexOf(10);
        var contains = data.Contains(5);
        data[1] = 20;
        var removed = data.Remove(5);

        // Assert
        data.Should().Equal(20);
        index.Should().Be(1);
        contains.Should().BeTrue();
        removed.Should().BeTrue();
        data.Count.Should().Be(1);
    }

    [Fact]
    public void Clear_ShouldEmptyTheCollection()
    {
        // Arrange
        var data = new BinaryData(new byte[] { 1, 2, 3 });

        // Act
        data.Clear();

        // Assert
        data.Count.Should().Be(0);
    }

    [Fact]
    public void GetEnumerator_ShouldEnumerateAllBytes()
    {
        // Arrange
        var data = new BinaryData(new byte[] { 100, 200 });

        // Act
        var list = data.ToList();

        // Assert
        list.Should().Equal(100, 200);
    }

    [Fact]
    public void CopyTo_ShouldCopyToProvidedArray()
    {
        // Arrange
        var data = new BinaryData(new byte[] { 7, 8, 9 });
        var target = new byte[5];

        // Act
        data.CopyTo(target, 1);

        // Assert
        target.Should().Equal(0, 7, 8, 9, 0);
    }
}