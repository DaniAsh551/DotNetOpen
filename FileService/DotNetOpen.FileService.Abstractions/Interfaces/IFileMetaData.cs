using DotNetOpen.FileService.Configuration;
using System;
using System.IO;

namespace DotNetOpen.FileService
{
    /// <summary>
    /// Metadata for a File on the FileSystem.
    /// </summary>
    public interface IFileMetaData
    {
        /// <summary>
        /// Indicates whether the File Exists on the FileSystem.
        /// </summary>
        bool Exists { get; }
        /// <summary>
        /// Raw File Size in Bytes.
        /// </summary>
        long RawFileSize { get; }
        /// <summary>
        /// File Size in a desired Unit.
        /// </summary>
        /// <param name="fileSizeUnit">The Unit for File Size calculation. Default: Byte</param>
        /// <returns></returns>
        double GetFileSize(FileSizeUnit fileSizeUnit = FileSizeUnit.Byte);
        /// <summary>
        /// Get the name of the file from FileSystem.
        /// </summary>
        string FileName { get; }
        /// <summary>
        /// Get the type of the file.
        /// </summary>
        string FileType { get; }
        /// <summary>
        /// Get the path of the Root Directory for File Storage.
        /// </summary>
        string Root { get; }
        /// <summary>
        /// Gets the absolute complete path of the File from the FileSystem.
        /// </summary>
        string AbsolutePath { get; }
        /// <summary>
        /// Gets the Time of creation of the file.
        /// </summary>
        DateTime CreatedTime { get; }
        /// <summary>
        /// Gets the time when the File was last accessed.
        /// </summary>
        DateTime LastAccessTime { get; }
        /// <summary>
        /// Gets the Time when the file was last modified.
        /// </summary>
        DateTime LastModifiedTime { get; }
        /// <summary>
        /// Gets the data from file in the form of a Stream.
        /// </summary>
        /// <param name="fileAccess">Whether the file should be opened in RO or WO or RW mode;</param>
        /// <returns>The data from file in the form of a Stream.</returns>
        Stream GetStream(FileAccess fileAccess = FileAccess.Read);
    }
}
