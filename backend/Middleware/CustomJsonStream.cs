using System.Text.Json;

namespace backend.Streams;

public class CustomJsonStream : Stream
{
    private readonly Stream _originalStream;
    private readonly HashSet<string> _seenKeys = new();
    private JsonReaderState _jsonReaderState;

    public CustomJsonStream(Stream originalStream)
    {
        _originalStream = originalStream;
    }

    public override bool CanRead => _originalStream.CanRead;
    public override bool CanSeek => _originalStream.CanSeek;
    public override bool CanWrite => false; // This stream is read-only
    public override long Length => _originalStream.Length;
    
    public override long Position
    {
        get => _originalStream.Position;
        set => throw new NotSupportedException();
    }

    public override void Flush() => _originalStream.Flush();

    public override long Seek(long offset, SeekOrigin origin) => 
        throw new NotSupportedException();

    public override void SetLength(long value) => 
        throw new NotSupportedException();

    public override int Read(byte[] buffer, int offset, int count)
    {
        int bytesRead = _originalStream.Read(buffer, offset, count);
        if (bytesRead > 0)
        {
            ProcessJson(new ReadOnlySpan<byte>(buffer, offset, bytesRead));
        }
        return bytesRead;
    }

    public override async ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken)
    {
        int bytesRead = await _originalStream.ReadAsync(buffer, cancellationToken);
        if (bytesRead > 0)
        {
            ProcessJson(buffer.Span.Slice(0, bytesRead));
        }
        return bytesRead;
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        throw new NotSupportedException("Writing is not supported in this stream.");
    }

    private void ProcessJson(ReadOnlySpan<byte> span)
    {
        Utf8JsonReader reader = new Utf8JsonReader(span, isFinalBlock: false, _jsonReaderState);
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                string key = reader.GetString()!;
                if (!_seenKeys.Add(key))
                {
                    throw new JsonException($"Duplicate key detected: {key}");
                }
            }
        }
        _jsonReaderState = reader.CurrentState;
    }
}