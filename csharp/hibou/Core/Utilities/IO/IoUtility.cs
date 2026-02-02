using System;
using System.IO;
using System.IO.Compression;

namespace Keane.CH.Framework.Core.Utilities.IO
{
    /// <summary>
    /// Encapsualtes io utility functions.
    /// </summary>
    /// <created by="Mark Morgan" date="01-Jan-2008" />
    public sealed class IoUtility
    {
        #region Ctor.

        private IoUtility() { }

        #endregion Ctor.

        #region Public Methods

        /// <summary>
        /// Deletes all files within the directory.
        /// </summary>
        /// <param name="directory">The directory from which all files will be deleted.</param>
        /// <param name="deleteSubDirectories">Flag indicating whether sub-directories wil be deleted or not.</param>
        public static void DeleteDirectoryFiles(
            string directory,
            bool deleteSubDirectories)
        {
            // Exit if the directory does not exist.
            if (!Directory.Exists(directory))
                return;

            // Delete sub-directories.
            if (deleteSubDirectories)
            {
                string[] subDirectories = Directory.GetDirectories(directory);
                for (int i = 0; i < subDirectories.Length; i++)
                {
                    DeleteDirectoryFiles(subDirectories[i], true);
                }
            }

            // Delete files.
            string[] files = Directory.GetFiles(directory);
            for (int i = 0; i < files.Length; i++)
            {
                File.Delete(files[i]);
            }
        }

        /// <summary>
        /// Deletes all files within the directory.
        /// </summary>
        /// <param name="directory">The directory from which all files will be deleted.</param>
        /// <remarks>Sub directories will not be deleted.</remarks>
        public static void DeleteDirectoryFiles(
            string directory)
        {
            DeleteDirectoryFiles(directory, false);
        }

        /// <summary>
        /// Compresses a stream.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="destination"></param>
        public static void Compress(Stream source, Stream destination)
        {
            using (DeflateStream output = new DeflateStream(destination, CompressionMode.Compress))
            {
                Pump(source, destination);
            }
        }

        /// <summary>
        /// Gets the byte buffer from the passed input stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        public static byte[] GetBufferFromStream(Stream source)
        {
            byte[] sourceBuffer = new byte[source.Length];
            source.Read(sourceBuffer, 0, (int)source.Length);
            return sourceBuffer;
        }

        /// <summary>
        /// Gets the zipped byte buffer from the file.
        /// </summary>
        /// <param name="filePath">The path to the file being deserialized.</param>
        public static byte[] GetBufferFromFile(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
                throw new ArgumentException("A non-existent file cannot be zipped.");

            // Unpack the file.
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return GetBufferFromStream(fs);
            }
        }

        /// <summary>
        /// Gets the zipped byte buffer from the file.
        /// </summary>
        /// <param name="sourceBuffer">The control buffer.</param>
        /// <returns></returns>
        public static byte[] GetCompressedBuffer(byte[] sourceBuffer)
        {
            if (sourceBuffer == null)
                throw new ArgumentNullException("sourceBuffer");

            // Zip and unpack the zipped stream.
            using (MemoryStream destination = new MemoryStream())
            {
                Compress(new MemoryStream(sourceBuffer), destination);
                return destination.ToArray();
            }
        }

        /// <summary>
        /// Gets the compressed byte buffer from the file stream.
        /// </summary>
        /// <param name="control">The input stream.</param>
        public static byte[] GetCompressedBufferFromStream(Stream source)
        {
            return GetCompressedBuffer(GetBufferFromStream(source));
        }

        /// <summary>
        /// Gets the compressed byte buffer from the file.
        /// </summary>
        /// <param name="filePath">The path to the file being deserialized.</param>
        public static byte[] GetCompressedBufferFromFile(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
                throw new ArgumentException("A non-existent file cannot be zipped.");

            return GetCompressedBuffer(GetBufferFromFile(filePath));
        }

        /// <summary>
        /// Gets the decompressed buffer from the passed compressed buffer.
        /// </summary>
        /// <param name="compressedBuffer">The compressed buffer.</param>
        /// <param name="decompressedByteLength">The decompressed length.</param>
        public static byte[] GetDecompressedBuffer(byte[] compressedBuffer,
                                                   int decompressedByteLength)
        {
            byte[] decompressedBuffer = new byte[decompressedByteLength];
            using (DeflateStream input = new DeflateStream(new MemoryStream(compressedBuffer), CompressionMode.Decompress, true))
            {
                input.BaseStream.Read(decompressedBuffer, 0, decompressedByteLength);
            }
            return decompressedBuffer;
        }

        /// <summary>
        /// Modifies the file last write time.
        /// </summary>
        /// <param name="filePath">The path to the file being touched.</param>
        public static void TouchFile(string filePath)
        {
            (new FileInfo(filePath)).LastWriteTimeUtc = DateTime.UtcNow;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Copues one stream to another.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <param name="output">The output stream.</param>
        private static void Pump(Stream input, Stream output)
        {
            input.Position = 0;
            byte[] bytes = new byte[4096];
            int n;
            while ((n = input.Read(bytes, 0, bytes.Length)) != 0)
            {
                output.Write(bytes, 0, n);
            }
        }

        #endregion Private Methods       
    }
}
