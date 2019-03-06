using DotNetOpen.FileService.Configuration;
using System;
using System.IO;

namespace DotNetOpen.FileService
{
    /// <summary>
    /// Configuration for File Handler
    /// </summary>
    public interface IFileServiceConfig
    {
        /// <summary>
        /// The mime type given the file data as a Stream.
        /// </summary>
        MimeTypeHelper.GetMimeTypeDelegate GetMimeType { get; }
        /// <summary>
        /// Maximum allowed upload size
        /// </summary>
        double MaxUploadSize { get; }
        /// <summary>
        /// Unit for Measuring file size
        /// </summary>
        FileSizeUnit FileSizeUnit { get; }
        /// <summary>
        /// FileSystem Root directory to save files in
        /// </summary>
        string RootDirectory { get; }
        /// <summary>
        /// Allowed File Extensions (eg: zip)
        /// </summary>
        string[] AllowedExtensions { get; }
        /// <summary>
        /// Determines whether this IFileServiceConfig is valid.
        /// </summary>
        /// <returns>Whether this IFileServiceConfig is valid.</returns>
        bool IsValid();
    }
}
