using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Reopenable file stream
    /// </summary>
    public class ReopenableFileStream : Stream
    {
        /// <summary>
        /// File stream
        /// </summary>
        private FileStream fileStream;

        /// <summary>
        /// File path
        /// </summary>
        public string FilePath { get; private set; }

        /// <summary>
        /// File access
        /// </summary>
        public FileAccess FileAccess { get; private set; }

        /// <summary>
        /// File share
        /// </summary>
        public FileShare FileShare { get; private set; }

        /// <summary>
        /// Is open
        /// </summary>
        public bool IsOpen { get; private set; } = true;

        /// <summary>
        /// Can read
        /// </summary>
        public override bool CanRead => fileStream.CanRead;

        /// <summary>
        /// Can seek
        /// </summary>
        public override bool CanSeek => fileStream.CanSeek;

        /// <summary>
        /// Can timeout
        /// </summary>
        public override bool CanTimeout => fileStream.CanTimeout;

        /// <summary>
        /// Can write
        /// </summary>
        public override bool CanWrite => fileStream.CanWrite;

        /// <summary>
        /// Length
        /// </summary>
        public override long Length => fileStream.Length;

        /// <summary>
        /// Position
        /// </summary>
        public override long Position
        {
            get => fileStream.Position;
            set => fileStream.Position = value;
        }

        /// <summary>
        /// Read timeout
        /// </summary>
        public override int ReadTimeout
        {
            get => fileStream.ReadTimeout;
            set => fileStream.ReadTimeout = value;
        }

        /// <summary>
        /// Write timeout
        /// </summary>
        public override int WriteTimeout
        {
            get => fileStream.WriteTimeout;
            set => fileStream.WriteTimeout = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileStream">File stream</param>
        /// <param name="filePath">File path</param>
        /// <param name="fileMode">File mode</param>
        /// <param name="fileAccess">File access</param>
        /// <param name="fileShare">File share</param>
        private ReopenableFileStream(FileStream fileStream, string filePath, FileAccess fileAccess, FileShare fileShare)
        {
            this.fileStream = fileStream;
            FilePath = filePath;
            FileAccess = fileAccess;
            FileShare = fileShare;
        }

        /// <summary>
        /// Open file
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <param name="fileMode">File mode</param>
        /// <param name="fileAccess">File access</param>
        /// <param name="fileShare">File share</param>
        /// <returns>Reopenable file stream if successful, otherwise "null"</returns>
        public static ReopenableFileStream Open(string filePath, FileMode fileMode, FileAccess fileAccess, FileShare fileShare)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }
            FileStream file_stream = null;
            try
            {
                file_stream = File.Open(filePath, fileMode, fileAccess, fileShare);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                if (file_stream != null)
                {
                    try
                    {
                        file_stream.Dispose();
                        file_stream = null;
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError(ex);
                    }
                }
            }
            return ((file_stream == null) ? null : (new ReopenableFileStream(file_stream, filePath, fileAccess, fileShare)));
        }

        /// <summary>
        /// Reopen file
        /// </summary>
        public void Reopen()
        {
            if (!IsOpen)
            {
                fileStream = File.Open(FilePath, FileMode.Open, FileAccess, FileShare);
                IsOpen = true;
            }
        }

        /// <summary>
        /// Begin reading bytes
        /// </summary>
        /// <param name="buffer">Buffer</param>
        /// <param name="offset">Offset in bytes</param>
        /// <param name="count">Count in bytes</param>
        /// <param name="callback">Asynchronous callback</param>
        /// <param name="state">State</param>
        /// <returns>Asynchronous result</returns>
        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state) => fileStream.BeginRead(buffer, offset, count, callback, state);

        /// <summary>
        /// Vegin writing bytes
        /// </summary>
        /// <param name="buffer">Buffer</param>
        /// <param name="offset">Offset in bytes</param>
        /// <param name="count">Count in bytes</param>
        /// <param name="callback">Asynchronous callback</param>
        /// <param name="state">State</param>
        /// <returns>Asynchronous result</returns>
        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state) => fileStream.BeginWrite(buffer, offset, count, callback, state);

        /// <summary>
        /// Close 
        /// </summary>
        public override void Close()
        {
            if (IsOpen)
            {
                fileStream.Close();
                IsOpen = false;
            }
        }

        /// <summary>
        /// Copy to stream (asynchronous)
        /// </summary>
        /// <param name="destination">Destination stream</param>
        /// <param name="bufferSize">Buffer size in bytes</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken) => fileStream.CopyToAsync(destination, bufferSize, cancellationToken);

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing">Is disposing</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                fileStream.Dispose();
                IsOpen = false;
            }
        }

        /// <summary>
        /// End reading bytes
        /// </summary>
        /// <param name="asyncResult">Asynchronous result</param>
        /// <returns>Number of bytes read</returns>
        public override int EndRead(IAsyncResult asyncResult) => fileStream.EndRead(asyncResult);

        /// <summary>
        /// End writing bytes
        /// </summary>
        /// <param name="asyncResult">Asynchronous result</param>
        public override void EndWrite(IAsyncResult asyncResult) => fileStream.EndWrite(asyncResult);

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>"true" if equivalent, otherwise "false"</returns>
        public override bool Equals(object obj) => fileStream.Equals(obj);

        /// <summary>
        /// Flush stream (asynchronous)
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task FlushAsync(CancellationToken cancellationToken) => fileStream.FlushAsync(cancellationToken);

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode() => fileStream.GetHashCode();

        /// <summary>
        /// Initialize lifetime service
        /// </summary>
        /// <returns>Result</returns>
        public override object InitializeLifetimeService() => fileStream.InitializeLifetimeService();

        /// <summary>
        /// Read bytes (asynchronous)
        /// </summary>
        /// <param name="buffer">Buffer</param>
        /// <param name="offset">Offset in bytes</param>
        /// <param name="count"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Number of bytes read task</returns>
        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) => fileStream.ReadAsync(buffer, offset, count, cancellationToken);

        /// <summary>
        /// Read byte
        /// </summary>
        /// <returns>Byte value if successful, otherwise "-1"</returns>
        public override int ReadByte() => fileStream.ReadByte();

        /// <summary>
        /// To string
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString() => fileStream.ToString();

        /// <summary>
        /// Write bytes (asynchronous)
        /// </summary>
        /// <param name="buffer">Buffer</param>
        /// <param name="offset">Offset in bytes</param>
        /// <param name="count">Count in bytes</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) => fileStream.WriteAsync(buffer, offset, count, cancellationToken);

        /// <summary>
        /// Write byte
        /// </summary>
        /// <param name="value">Byte value</param>
        public override void WriteByte(byte value) => fileStream.WriteByte(value);

        /// <summary>
        /// Flush
        /// </summary>
        public override void Flush() => fileStream.Flush();

        /// <summary>
        /// Read bytes
        /// </summary>
        /// <param name="buffer">Buffer</param>
        /// <param name="offset">Offset in bytes</param>
        /// <param name="count">Count in bytes</param>
        /// <returns>Number of bytes read</returns>
        public override int Read(byte[] buffer, int offset, int count) => fileStream.Read(buffer, offset, count);

        /// <summary>
        /// Seek in stream
        /// </summary>
        /// <param name="offset">Offset in bytes</param>
        /// <param name="origin">Seek origin</param>
        /// <returns>New stream position</returns>
        public override long Seek(long offset, SeekOrigin origin) => fileStream.Seek(offset, origin);

        /// <summary>
        /// Set stream length
        /// </summary>
        /// <param name="value">Value</param>
        public override void SetLength(long value) => fileStream.SetLength(value);

        /// <summary>
        /// Write bytes
        /// </summary>
        /// <param name="buffer">Buffer</param>
        /// <param name="offset">Offset in bytes</param>
        /// <param name="count">Count in bytes</param>
        public override void Write(byte[] buffer, int offset, int count) => fileStream.Write(buffer, offset, count);
    }
}
