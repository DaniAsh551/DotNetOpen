using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dani551.Open.FileService;

namespace Dani551.Open.FileService
{
    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public class FileService : IFileService
    {
        private readonly IFileServiceConfig _fileServiceConfig;
        
        /// <summary>
        /// The configuration for a File Handler / Service
        /// </summary>
        /// <param name="fileServiceConfig">The configuration to be used</param>
        public FileService(IFileServiceConfig fileServiceConfig)
        {
            this._fileServiceConfig = fileServiceConfig;
        }

        private void ValidateConfiguration()
        {
            if (_fileServiceConfig == null) throw new ArgumentNullException(nameof(_fileServiceConfig), "File Service configuration was not supplied");
            else if(!_fileServiceConfig.IsValid()) throw new ArgumentException(nameof(_fileServiceConfig), "File Service configuration is not valid");
        }
        

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public IFileMetaData GetFile(string fileName, string fileType, bool throwOnException = true, bool throwOnNotFound = true)
        {
            try
            {
                string absolutePath = _fileServiceConfig.RootDirectory + Path.DirectorySeparatorChar + fileName;
                return new FileMetaData(_fileServiceConfig, fileName, fileType, throwOnException && throwOnNotFound);
            }
            catch (Exception ex)
            {
                if (ex is FileNotFoundException && !throwOnNotFound) return null;
                if (throwOnException) throw ex;
                return null;
            }
        }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public IFileMetaData[] GetAllFiles(string fileType, bool throwOnException = true, bool throwOnNotFound = true)
        {
            try
            {
               return Directory.EnumerateFiles(_fileServiceConfig.RootDirectory + Path.DirectorySeparatorChar + fileType)?.Select(x => new FileMetaData(_fileServiceConfig, x.Split(Path.DirectorySeparatorChar)?.LastOrDefault(), x.Split(Path.DirectorySeparatorChar)?.Reverse()?.Skip(1)?.FirstOrDefault(), throwOnException))?.ToArray();
            }
            catch (Exception ex)
            {
                if (ex is FileNotFoundException && !throwOnNotFound) return null;
                if (throwOnException) throw ex;
                return null;
            }
        }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public IFileMetaData[] GetAllFiles(bool throwOnException = true, bool throwOnNotFound = true)
        {
            try
            {
                return Directory.EnumerateDirectories(_fileServiceConfig.RootDirectory)?.SelectMany(y =>
                    Directory.EnumerateFiles(y)?.Select(x => new FileMetaData(_fileServiceConfig, x.Split(Path.DirectorySeparatorChar)?.LastOrDefault(), x.Split(Path.DirectorySeparatorChar)?.Reverse()?.Skip(1)?.FirstOrDefault(), throwOnException))
                )?.ToArray();
            }
            catch (Exception ex)
            {
                if (ex is FileNotFoundException && !throwOnNotFound) return null;
                if (throwOnException) throw ex;
                return null;
            }
        }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public IFileMetaData Create(string fileType, byte[] bytes, bool throwOnException = true)
        {
            try
            {
                if (bytes.Length > _fileServiceConfig.MaxUploadSize) throw new FileSizeLimitExceedException(Convert.ToInt64(_fileServiceConfig.MaxUploadSize), bytes.Length);
                string fileName = Guid.NewGuid().ToString();
                IFileMetaData fileMetaData = new FileMetaData(_fileServiceConfig, fileName, fileType, throwOnException);
                if (!File.Exists(fileMetaData.AbsolutePath))
                {
                    new FileInfo(fileMetaData.AbsolutePath).Directory.Create();
                    File.WriteAllBytes(fileMetaData.AbsolutePath, bytes);
                    fileMetaData = new FileMetaData(_fileServiceConfig, fileName, fileType, throwOnException);
                }
                
                return fileMetaData;
            }
            catch (Exception ex)
            {
                if (throwOnException) throw ex;
                return null;
            }
        }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public async Task<IFileMetaData> CreateAsync(string fileType, byte[] bytes, bool throwOnException = true)
        {
            try
            {
                if (bytes.Length > _fileServiceConfig.MaxUploadSize) throw new FileSizeLimitExceedException(Convert.ToInt64(_fileServiceConfig.MaxUploadSize), bytes.Length);
                string fileName = Guid.NewGuid().ToString();
                IFileMetaData fileMetaData = new FileMetaData(_fileServiceConfig, fileName, fileType, false);
                if (!File.Exists(fileMetaData.AbsolutePath))
                {
                    new FileInfo(fileMetaData.AbsolutePath).Directory.Create();
                    await Task.Factory.StartNew(() => File.WriteAllBytes(fileMetaData.AbsolutePath, bytes));
                    fileMetaData = new FileMetaData(_fileServiceConfig, fileName, fileType, throwOnException);
                }
                
                return fileMetaData;
            }
            catch (Exception ex)
            {
                if (throwOnException) throw ex;
                return null;
            }
        }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public IFileMetaData Create(string fileName, string fileType, byte[] bytes, bool throwOnException = true)
        {
            try
            {
                if (bytes.Length > _fileServiceConfig.MaxUploadSize) throw new FileSizeLimitExceedException(Convert.ToInt64(_fileServiceConfig.MaxUploadSize), bytes.Length);
                IFileMetaData fileMetaData = new FileMetaData(_fileServiceConfig, fileName, fileType, throwOnException);
                if (!fileMetaData.Exists)
                {
                    new FileInfo(fileMetaData.AbsolutePath).Directory.Create();
                    File.WriteAllBytes(fileMetaData.AbsolutePath, bytes);
                    fileMetaData = new FileMetaData(_fileServiceConfig, fileName, fileType, throwOnException);
                }
                
                return fileMetaData;
            }
            catch (Exception ex)
            {
                if (throwOnException) throw ex;
                return null;
            }
        }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public async Task<IFileMetaData> CreateAsync(string fileName, string fileType, byte[] bytes, bool throwOnException = true)
        {
            try
            {
                if (bytes.Length > _fileServiceConfig.MaxUploadSize) throw new FileSizeLimitExceedException(Convert.ToInt64(_fileServiceConfig.MaxUploadSize), bytes.Length);
                IFileMetaData fileMetaData = new FileMetaData(_fileServiceConfig, fileName, fileType, false);
                if (!fileMetaData.Exists)
                {
                    new FileInfo(fileMetaData.AbsolutePath).Directory.Create();
                    await Task.Factory.StartNew(() => File.WriteAllBytes(fileMetaData.AbsolutePath, bytes));
                    fileMetaData = new FileMetaData(_fileServiceConfig, fileName, fileType, throwOnException);
                }
                
                return fileMetaData;
            }
            catch (Exception ex)
            {
                if (throwOnException) throw ex;
                return null;
            }
        }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public IFileMetaData Update(string fileName, string fileType, byte[] bytes, bool throwOnException = true, bool throwOnNotFound = true)
        {
            try
            {
                if (bytes.Length > _fileServiceConfig.MaxUploadSize) throw new FileSizeLimitExceedException(Convert.ToInt64(_fileServiceConfig.MaxUploadSize), bytes.Length);
                string absolutePath = _fileServiceConfig.RootDirectory + Path.DirectorySeparatorChar + fileType + Path.DirectorySeparatorChar+ fileName;
                Delete(fileName, fileType, throwOnException, true);
                File.WriteAllBytes(absolutePath, bytes);
                IFileMetaData fileMetaData = new FileMetaData(_fileServiceConfig, fileName, fileType, throwOnException);
                return fileMetaData;
            }
            catch (Exception ex)
            {
                if (throwOnException) throw ex;
                return null;
            }
        }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public async Task<IFileMetaData> UpdateAsync(string fileName, string fileType, byte[] bytes, bool throwOnException = true, bool throwOnNotFound = true)
        {
            try
            {
                if (bytes.Length > _fileServiceConfig.MaxUploadSize) throw new FileSizeLimitExceedException(Convert.ToInt64(_fileServiceConfig.MaxUploadSize), bytes.Length);
                string absolutePath = _fileServiceConfig.RootDirectory + Path.DirectorySeparatorChar + fileType + Path.DirectorySeparatorChar + fileName;
                Delete(fileName, fileType,throwOnException, true);
                await Task.Factory.StartNew(() => File.WriteAllBytes(absolutePath, bytes));
                IFileMetaData fileMetaData = new FileMetaData(_fileServiceConfig, fileName, fileType, throwOnException);
                return fileMetaData;
            }
            catch (Exception ex)
            {
                if (throwOnException) throw ex;
                return null;
            }
        }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public IFileMetaData Move(string fileName, string oldFileType, string newFileType, bool throwOnException = true, bool throwOnNotFound = true)
        {
            try
            {
                string absolutePath = _fileServiceConfig.RootDirectory + Path.DirectorySeparatorChar + oldFileType + Path.DirectorySeparatorChar + fileName;
                if(!File.Exists(absolutePath)) throw new FileNotFoundException($"The File \"{absolutePath}\" was not found.");
                File.Move(absolutePath, _fileServiceConfig.RootDirectory + Path.DirectorySeparatorChar + newFileType + Path.DirectorySeparatorChar + fileName);
                IFileMetaData fileMetaData = new FileMetaData(_fileServiceConfig, fileName, newFileType, throwOnException);
                return fileMetaData;
            }
            catch (Exception ex)
            {
                if (ex is FileNotFoundException)
                {
                    if (throwOnNotFound) throw ex;
                    return null;
                }
                if (throwOnException) throw ex;
                return null;
            }
        }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public void Delete(string fileName, string fileType, bool throwOnException = true, bool throwOnNotFound = false)
        {
            try
            {
                string absolutePath = _fileServiceConfig.RootDirectory + Path.DirectorySeparatorChar + fileType + Path.DirectorySeparatorChar+ fileName;
                if (!File.Exists(absolutePath))
                {
                    if (throwOnNotFound) throw new FileNotFoundException($"The File \"{absolutePath}\" was not found.");
                    return;
                }
                File.Delete(absolutePath);
            }
            catch (Exception ex)
            {
                if(ex is FileNotFoundException)
                {
                    if (throwOnNotFound) throw ex;
                    return;
                }
                if (throwOnException) throw ex;
            }
        }
    }
}
