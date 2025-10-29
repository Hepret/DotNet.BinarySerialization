namespace BinaryData;

/// <summary>
/// Interface for a mutable sequence of bytes with extended functionality,
/// including support for ranges and streams.
/// </summary>
public interface IBinaryData : IList<byte>
{
    /// <summary>
    /// Appends a sequence of bytes to the end of the collection.
    /// </summary>
    /// <param name="bytes">The collection of bytes to append.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="bytes"/> is <c>null</c>.</exception>
    void AddRange(IEnumerable<byte> bytes);

    /// <summary>
    /// Inserts a sequence of bytes at the specified position in the collection.
    /// </summary>
    /// <param name="index">The index at which insertion begins.</param>
    /// <param name="bytes">The collection of bytes to insert.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="index"/> is less than zero or greater than the number of elements.
    /// </exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="bytes"/> is <c>null</c>.</exception>
    void InsertRange(int index, IEnumerable<byte> bytes);

    /// <summary>
    /// Returns a new <see cref="BinaryData"/> instance containing a subset of bytes
    /// specified by a <see cref="Range"/>.
    /// </summary>
    /// <param name="range">The range of indices to extract.</param>
    /// <returns>A new <see cref="IBinaryData"/> object containing the requested slice of data.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the range is outside the valid index bounds.
    /// </exception>
    IBinaryData this[Range range] { get; }

    /// <summary>
    /// Returns a <see cref="Stream"/> containing a copy of all bytes in memory.
    /// The stream is read-only and positioned at the beginning.
    /// </summary>
    /// <remarks>
    /// Each call creates a new <see cref="MemoryStream"/> instance.
    /// Modifications to the original data after obtaining the stream are not reflected in the stream.
    /// </remarks>
    Stream Stream { get; }
}