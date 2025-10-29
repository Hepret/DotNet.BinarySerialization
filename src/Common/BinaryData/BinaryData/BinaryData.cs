using System.Collections;

namespace BinaryData;

/// <summary>
/// A mutable sequence of bytes (implementation of the <see cref="IBinaryData"/> interface).
/// </summary>
public class BinaryData : IBinaryData
{
    /// <summary>
    /// Internal byte storage.
    /// </summary>
    private readonly List<byte> _bytes = [];

    /// <inheritdoc />
    public void Add(byte @byte)
    {
        _bytes.Add(@byte);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryData"/> class.
    /// </summary>
    public BinaryData() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryData"/> class from the specified collection of bytes.
    /// </summary>
    /// <param name="bytes">The source collection of bytes.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="bytes"/> is <c>null</c>.</exception>
    public BinaryData(IEnumerable<byte> bytes)
    {
        _bytes.AddRange(bytes ?? throw new ArgumentNullException(nameof(bytes)));
    }

    /// <inheritdoc />
    public void AddRange(IEnumerable<byte> bytes)
    {
        _bytes.AddRange(bytes ?? throw new ArgumentNullException(nameof(bytes)));
    }

    /// <inheritdoc />
    public void Clear()
    {
        _bytes.Clear();
    }

    /// <inheritdoc />
    public bool Contains(byte item)
    {
        return _bytes.Contains(item);
    }

    /// <inheritdoc />
    public void CopyTo(byte[] array, int arrayIndex)
    {
        _bytes.CopyTo(array, arrayIndex);
    }

    /// <inheritdoc />
    public bool Remove(byte item)
    {
        return _bytes.Remove(item);
    }

    /// <inheritdoc />
    public int Count => _bytes.Count;

    /// <inheritdoc />
    public bool IsReadOnly => false;

    /// <inheritdoc />
    public int IndexOf(byte item)
    {
        return _bytes.IndexOf(item);
    }

    /// <inheritdoc />
    public void Insert(int index, byte @byte)
    {
        _bytes.Insert(index, @byte);
    }

    /// <inheritdoc />
    public void RemoveAt(int index)
    {
        _bytes.RemoveAt(index);
    }

    /// <inheritdoc />
    public void InsertRange(int index, IEnumerable<byte> bytes)
    {
        _bytes.InsertRange(index, bytes);
    }

    /// <inheritdoc />
    public IBinaryData this[Range range]
    {
        get
        {
            var (offset, length) = range.GetOffsetAndLength(_bytes.Count);
            var data = _bytes.GetRange(offset, length);
            return new BinaryData(data);
        }
    }

    /// <inheritdoc />
    public Stream Stream => new MemoryStream(_bytes.ToArray(), false);

    public byte this[int index]
    {
        get => _bytes[index];
        set => _bytes[index] = value;
    }

    /// <inheritdoc />
    public IEnumerator<byte> GetEnumerator()
    {
        return _bytes.GetEnumerator();
    }

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}