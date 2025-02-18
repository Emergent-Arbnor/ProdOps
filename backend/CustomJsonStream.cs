using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace backend
{
    /// <summary>
    /// Custom stream wrapper class to intercept and process JSON data during a stream read.
    /// This stream ensures that no duplicate JSON keys are encountered by checking
    /// each key as it is read, throwing an exception if a duplicate is found.
    /// </summary>
    public class CustomJsonStream : Stream
    {
        // Create a set to keep track of all keys from the JSON
        private HashSet<string> _seenKeys = new HashSet<string>();
        private JsonReaderState _jsonReaderState;


        private readonly Stream _originalStream;
        public override bool CanRead => _originalStream.CanRead;
        public override bool CanSeek => _originalStream.CanSeek;
        public override bool CanWrite => _originalStream.CanWrite;
        public override void Flush() => _originalStream.Flush();
        public override long Length => _originalStream.Length;

        
        public override long Position 
        {
            get { return _originalStream.Position; }
            set { _originalStream.Position = value; }
            
        }
        public override long Seek(long offset, System.IO.SeekOrigin origin)
        {
            return _originalStream.Seek(offset, origin);
        }
        public override void SetLength(long value)
        {
            _originalStream.SetLength(value);
        }
        
        public CustomJsonStream(Stream originalStream)
        {
            _originalStream = originalStream;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("Synchronous reading is not allowed. Use ReadAsync instead.");
        }

        public override async ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken)
        {
            int bytesRead = await _originalStream.ReadAsync(buffer, cancellationToken);
            if (bytesRead == 0)
            {
                return 0;
            }

            // Process JSON synchronously to avoid 'ref struct' async issues
            ProcessJson(buffer.Span.Slice(0, bytesRead));

            return bytesRead;
        }

        private void ProcessJson(ReadOnlySpan<byte> span)
        {

            Utf8JsonReader reader = new Utf8JsonReader(span, isFinalBlock: true, _jsonReaderState);
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string key = reader.GetString()!;
                    if (!_seenKeys.Add(key))
                    {
                        throw new InvalidOperationException($"Duplicate key detected: {key}");
                    }
                }
            }

            // Update state for next read
            _jsonReaderState = reader.CurrentState;
        }


        public override void Write(byte[] buffer, int offset, int count)
        {
            _originalStream.Write(buffer, offset, count);
        }
    }
}
