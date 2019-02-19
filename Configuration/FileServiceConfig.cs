using System;

namespace Dani551.Open.FileService
{
    public class FileServiceConfig : IFileServiceConfig
    {
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public double MaxUploadSize { get; set; }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public FileSizeUnit FileSizeUnit { get; set; }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public string RootDirectory { get; set; }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public string[] AllowedExtensions { get; set; }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public Uri ServerAddress { get; set; }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public string FileGetActionPath { get; set; }
    }
}
