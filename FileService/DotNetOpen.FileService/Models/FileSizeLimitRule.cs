using DotNetOpen.FileService.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotNetOpen.FileService
{
    /// <summary>
    /// A rule which enforces the filesize limits.
    /// </summary>
    public class FileSizeLimitRule : IRule<FileSizeLimitExceedException>
    {
        /// <inheritdoc/>
        public string Name { get => nameof(FileSizeLimitRule); }

        /// <inheritdoc/>
        public void Execute(IFileServiceConfig fileServiceConfig, Stream inputStream, string fileType, string fileName = null)
        {
            var maxFileSize = fileServiceConfig.MaxUploadSize;
            var fileSizeUnit = fileServiceConfig.FileSizeUnit;

            var fileSize = inputStream.Length;
            Validate(maxFileSize, fileSizeUnit, fileSize);
        }

        /// <inheritdoc/>
        public void Execute(IFileServiceConfig fileServiceConfig, byte[] inputBytes, string fileType, string fileName = null)
        {
            var maxFileSize = fileServiceConfig.MaxUploadSize;
            var fileSizeUnit = fileServiceConfig.FileSizeUnit;

            var fileSize = inputBytes.Length;
            Validate(maxFileSize, fileSizeUnit, fileSize);
        }

        private void Validate(double maxFileSize, FileSizeUnit fileSizeUnit, long fileSize)
        {
            var fileSizeInUnits = Math.Pow((double)1024, (double)-1 * ((double)fileSizeUnit)) * fileSize;
            if (fileSizeInUnits > maxFileSize)
                throw new FileSizeLimitExceedException(fileSizeUnit, (long)maxFileSize, fileSize);
        }
    }
}
