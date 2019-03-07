using DotNetOpen.FileService.Configuration;
using System;
using System.IO;

namespace DotNetOpen.FileService
{
    /// <summary>
    /// The default <see cref="IFileServiceConfig"/>.
    /// </summary>
    public class FileServiceConfig : IFileServiceConfig
    {

        public FileServiceConfig(MimeTypeHelper.GetMimeTypeDelegate getMimeTypeDelegate, double maxUploadSize, FileSizeUnit fileSizeUnit, string rootDirectory, IRuleContainer rules, params string[] allowedExtensions)
        {
            GetMimeType = getMimeTypeDelegate;
            MaxUploadSize = maxUploadSize;
            FileSizeUnit = fileSizeUnit;
            RootDirectory = rootDirectory;
            Rules = rules;

            try
            {
                Rules.AddRule<FileSizeLimitRule>();
                Rules.AddRule<FileExtensionValidationRule>();
            }
            catch (Exception)
            {
            }

            AllowedExtensions = allowedExtensions;
        }

        public FileServiceConfig(MimeTypeHelper.GetMimeTypeDelegate getMimeTypeDelegate, double maxUploadSize, FileSizeUnit fileSizeUnit, string rootDirectory, params string[] allowedExtensions)
        {
            GetMimeType = getMimeTypeDelegate;
            MaxUploadSize = maxUploadSize;
            FileSizeUnit = fileSizeUnit;
            RootDirectory = rootDirectory;
            Rules = new RuleContainer();
            Rules.AddRule<FileSizeLimitRule>();
            Rules.AddRule<FileExtensionValidationRule>();
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

        /// <inheritdoc />
        public IRuleContainer Rules { get; }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public bool IsValid()
            => Rules != null && Rules.Length > 1 && GetMimeType != null && MaxUploadSize > 0 && !string.IsNullOrWhiteSpace(RootDirectory) && Directory.Exists(RootDirectory) && (AllowedExtensions?.Length ?? 0) > 0;
    }
}
