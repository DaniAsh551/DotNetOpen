using System;

namespace Dani551.Open.FileService
{
    /// <summary>
    /// Configuration for File Handler
    /// </summary>
    public interface IFileServiceConfig
    {
        /// <summary>
        /// Maximum allowed upload size
        /// </summary>
        double MaxUploadSize { get; set; }
        /// <summary>
        /// Unit for Measuring file size
        /// </summary>
        FileSizeUnit FileSizeUnit { get; set; }
        /// <summary>
        /// FileSystem Root directory to save files in
        /// </summary>
        string RootDirectory { get; set; }
        /// <summary>
        /// Allowed File Extensions (eg: zip)
        /// </summary>
        string[] AllowedExtensions { get; set; }
        /// <summary>
        /// The Action on File Controller on the Webapp to get a file
        /// </summary>
        string FileGetActionPath { get; set; }
        /// <summary>
        /// Gets the current Server Host Adress
        /// </summary>
        Uri ServerAddress { get;}
    }
}
