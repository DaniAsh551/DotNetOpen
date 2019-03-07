using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DotNetOpen.FileService
{
    /// <summary>
    /// Enforces file name extensions on the files.
    /// </summary>
    public class FileExtensionValidationRule : IRule<InvalidExtensionException>
    {
        /// <inheritdoc/>
        public string Name => nameof(FileExtensionValidationRule);
        /// <inheritdoc/>
        public void Execute(IFileServiceConfig fileServiceConfig, Stream inputStream, string fileType, string fileName = null)
        {
            Validate(fileServiceConfig, fileName);
        }
        /// <inheritdoc/>
        public void Execute(IFileServiceConfig fileServiceConfig, byte[] inputBytes, string fileType, string fileName = null)
        {
            Validate(fileServiceConfig, fileName);
        }

        private void Validate(IFileServiceConfig fileServiceConfig, string fileName)
        {
            if (string.IsNullOrEmpty(fileName) || !fileName.Contains('.'))
                return;

            var extension = Path.GetExtension(fileName)?.ToLower()?.Remove(0, 1);
            var isValid = string.IsNullOrEmpty(extension) ? true : fileServiceConfig?.AllowedExtensions?.Any(x => x == extension) ?? false;

            if (!isValid)
                throw new InvalidExtensionException(extension, fileServiceConfig.AllowedExtensions);
        }
    }
}
