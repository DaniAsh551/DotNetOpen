using DotNetOpen.FileService.Configuration;
using System;
using System.IO;
using System.Linq;

namespace DotNetOpen.FileService
{
    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public class FileMetaData : IFileMetaData
    {
        /// <summary>
        /// Generate file meta data from a file on the filesystem
        /// </summary>
        /// <param name="fileName">The Name of the file on the FileSystem in the Root Directory (Including extension)</param>
        /// <param name="fileType">The type of the File</param>
        /// <param name="fileServiceConfig">Associated File Handler's Configuration</param>
        /// <param name="throwOnNotFound">Indicates whether an Exception should be thrown when the file is not found. Default: true</param>
        public FileMetaData(IFileServiceConfig fileServiceConfig, string fileName, string fileType, bool? throwOnNotFound = true)
        {
            char seperator = Path.DirectorySeparatorChar;
            this.Root = fileServiceConfig.RootDirectory.LastOrDefault() == seperator ? fileServiceConfig.RootDirectory.Remove(fileServiceConfig.RootDirectory.Length - 1, 1) : fileServiceConfig.RootDirectory;
            this.FileType = fileType;
            this.FileName = fileName;
            if (File.Exists(AbsolutePath))
            {
                SetFileInfo();
            }
            else
            {
                if (throwOnNotFound.Value) throw new FileNotFoundException("The specified file does not exist on the File System", this.AbsolutePath);
            }
        }
        /// <summary>
        /// Generate file metadata from bytes by writing it on to the FileSystem
        /// </summary>
        /// <param name="fileServiceConfig">>Associated File Handler's Configuration</param>
        /// <param name="bytes">The bytes of the file</param>
        /// <param name="fileType">The Extension of the file (without '.'). eg:zip</param>
        /// <param name="throwOnException">Indicates whether an Exception should be thrown when the file is not found. Default: true</param>
        public FileMetaData(IFileServiceConfig fileServiceConfig, byte[] bytes, string fileType, bool? throwOnException = true)
        {
            char seperator = Path.DirectorySeparatorChar;
            this.Root = fileServiceConfig.RootDirectory.LastOrDefault() == seperator ? fileServiceConfig.RootDirectory.Remove(fileServiceConfig.RootDirectory.Length - 1, 1) : fileServiceConfig.RootDirectory;
            this.FileType = fileType;
            string fileName = Guid.NewGuid().ToString() + '.' + fileType;
            try
            {
                File.WriteAllBytes(AbsolutePath, bytes);
                SetFileInfo();
            }
            catch (Exception ex)
            {
                if (throwOnException.Value) throw ex;
            }
            
        }
        /// <summary>
        /// Generate file metadata from bytes by writing it on to the FileSystem
        /// </summary>
        /// <param name="fileServiceConfig">>Associated File Handler's Configuration</param>
        /// <param name="bytes">The bytes of the file</param>
        /// <param name="fileName">The Name of the file on the FileSystem in the Root Directory (Including extension)</param>
        /// <param name="fileType">The Extension of the file (without '.'). eg:zip</param>
        /// <param name="throwOnException">Indicates whether an Exception should be thrown when the file is not found. Default: true</param>
        public FileMetaData(IFileServiceConfig fileServiceConfig, byte[] bytes, string fileName, string fileType, bool? throwOnException = true)
        {
            char seperator = Path.DirectorySeparatorChar;
            this.Root = fileServiceConfig.RootDirectory.LastOrDefault() == seperator ? fileServiceConfig.RootDirectory.Remove(fileServiceConfig.RootDirectory.Length - 1, 1) : fileServiceConfig.RootDirectory;
            this.FileType = fileType;
            this.FileName = fileName;
            try
            {
                File.WriteAllBytes(AbsolutePath, bytes);
                SetFileInfo();
            }
            catch (Exception ex)
            {
                if (throwOnException.Value) throw ex;
            }

        }
        /// <summary>
        /// Generate file metadata from bytes by writing it on to the FileSystem
        /// </summary>
        /// <param name="fileServiceConfig">>Associated File Handler's Configuration</param>
        /// <param name="stream">The stream of the file</param>
        /// <param name="fileType">The Extension of the file (without '.'). eg:zip</param>
        /// <param name="throwOnException">Indicates whether an Exception should be thrown when the file is not found. Default: true</param>
        public FileMetaData(IFileServiceConfig fileServiceConfig, Stream stream, string fileType, bool? throwOnException = true)
        {
            char seperator = Path.DirectorySeparatorChar;
            this.Root = fileServiceConfig.RootDirectory.LastOrDefault() == seperator ? fileServiceConfig.RootDirectory.Remove(fileServiceConfig.RootDirectory.Length - 1, 1) : fileServiceConfig.RootDirectory;
            this.FileType = fileType;
            string fileName = Guid.NewGuid().ToString() + '.' + fileType;
            try
            {
                if (File.Exists(AbsolutePath))
                    File.Delete(AbsolutePath);

                CopyFromStream(ref stream);
                SetFileInfo();
            }
            catch (Exception ex)
            {
                if (throwOnException.Value) throw ex;
            }

        }
        /// <summary>
        /// Generate file metadata from bytes by writing it on to the FileSystem
        /// </summary>
        /// <param name="fileServiceConfig">>Associated File Handler's Configuration</param>
        /// <param name="stream">The stream of the file</param>
        /// <param name="fileName">The Name of the file on the FileSystem in the Root Directory (Including extension)</param>
        /// <param name="fileType">The Extension of the file (without '.'). eg:zip</param>
        /// <param name="throwOnException">Indicates whether an Exception should be thrown when the file is not found. Default: true</param>
        public FileMetaData(IFileServiceConfig fileServiceConfig, Stream stream, string fileName, string fileType, bool? throwOnException = true)
        {
            char seperator = Path.DirectorySeparatorChar;
            this.Root = fileServiceConfig.RootDirectory.LastOrDefault() == seperator ? fileServiceConfig.RootDirectory.Remove(fileServiceConfig.RootDirectory.Length - 1, 1) : fileServiceConfig.RootDirectory;
            this.FileType = fileType;
            this.FileName = fileName;
            try
            {
                if (File.Exists(AbsolutePath))
                    File.Delete(AbsolutePath);

                CopyFromStream(ref stream);
                SetFileInfo();
            }
            catch (Exception ex)
            {
                if (throwOnException.Value) throw ex;
            }

        }
        /// <summary>
        /// Create a new FileData object
        /// </summary>
        public FileMetaData()
        {
        }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public bool Exists => File.Exists(this.AbsolutePath);
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public long RawFileSize { get; internal set; }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public string FileName { get; internal set; }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public string FileType { get; internal set; }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public string Root { get; internal set; }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public string AbsolutePath => !string.IsNullOrWhiteSpace(Root) && !string.IsNullOrWhiteSpace(FileName) && !string.IsNullOrWhiteSpace(FileType) ?  Root + Path.DirectorySeparatorChar + FileType + Path.DirectorySeparatorChar + FileName : null;
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public DateTime CreatedTime { get; internal set; }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public DateTime LastAccessTime { get; internal set; }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public DateTime LastModifiedTime { get; internal set; }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public double GetFileSize(FileSizeUnit fileSizeUnit = FileSizeUnit.Byte)
        {
            double power = ((int)fileSizeUnit);
            double divisor = Math.Pow(1024.0d, power);
            return RawFileSize/divisor;
        }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public Stream GetStream(FileAccess fileAccess = FileAccess.Read)
            => new FileStream(AbsolutePath, FileMode.Open, fileAccess);

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public void RefreshFileInfo()
        {
            SetFileInfo();
        }

        /// <summary>
        /// Gets the Information about the file from the filesystem.
        /// </summary>
        private void SetFileInfo()
        {
            CreatedTime = File.GetCreationTime(AbsolutePath);
            LastAccessTime = File.GetLastAccessTime(AbsolutePath);
            LastModifiedTime = File.GetLastWriteTime(AbsolutePath);
            RawFileSize = new FileInfo(AbsolutePath).Length;
        }

        private void CopyFromStream(ref Stream stream)
        {
            using (var fstream = File.Open(AbsolutePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                stream.Position = 0;
                stream.CopyTo(fstream);
                fstream.Flush();
            }
        }
    }
}
