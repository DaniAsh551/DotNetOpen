using Dani551.Open.FileService.Configuration;
using System;
using System.IO;

namespace Dani551.Open.FileService
{
    public class FileServiceConfig : IFileServiceConfig
    {

        public FileServiceConfig(MimeTypeHelper.GetMimeTypeDelegate getMimeTypeDelegate, double maxUploadSize, FileSizeUnit fileSizeUnit, string rootDirectory, params string[] allowedExtensions)
        {
            GetMimeType = getMimeTypeDelegate;
            MaxUploadSize = maxUploadSize;
            FileSizeUnit = fileSizeUnit;
            RootDirectory = rootDirectory;
            AllowedExtensions = allowedExtensions;
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public double MaxUploadSize { get; }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public FileSizeUnit FileSizeUnit { get; }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public string RootDirectory { get; }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public string[] AllowedExtensions { get; }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public MimeTypeHelper.GetMimeTypeDelegate GetMimeType { get; }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public bool IsValid()
            => GetMimeType != null && MaxUploadSize > 0 && !string.IsNullOrWhiteSpace(RootDirectory) && Directory.Exists(RootDirectory) && (AllowedExtensions?.Length ?? 0) > 0;
    }
}
