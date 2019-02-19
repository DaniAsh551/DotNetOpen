using System.Threading.Tasks;

namespace Dani551.Open.FileService
{
    /// <summary>
    /// The Handler for handling all file related IO tasks
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Get an already existing file on the FileSystem
        /// </summary>
        /// <param name="fileName">Name of the file</param>
        /// <param name="fileType">The type of the file</param>
        /// <param name="throwOnException">Indicates whether an Exception should be thrown upon errors. Default:true</param>
        /// <param name="throwOnNotFound">Indicates whether an Exeption should be thrown if the file is not found. Default:true</param>
        /// <returns>IFileMetaData</returns>
        IFileMetaData GetFile(string fileName, string fileType, bool throwOnException = true, bool throwOnNotFound = true);
        /// <summary>
        /// Get all existing files on the FileSystem by File Type
        /// </summary>
        /// <param name="fileType">The type of the files</param>
        /// <param name="throwOnException">Indicates whether an Exception should be thrown upon errors. Default:true</param>
        /// <param name="throwOnNotFound">Indicates whether an Exeption should be thrown if the file is not found. Default:true</param>
        /// <returns>IFileMetaData</returns>
        IFileMetaData[] GetAllFiles(string fileType, bool throwOnException = true, bool throwOnNotFound = true);
        /// <summary>
        /// Get all existing files on the FileSystem
        /// </summary>
        /// <param name="throwOnException">Indicates whether an Exception should be thrown upon errors. Default:true</param>
        /// <param name="throwOnNotFound">Indicates whether an Exeption should be thrown if the file is not found. Default:true</param>
        /// <returns>IFileMetaData</returns>
        IFileMetaData[] GetAllFiles(bool throwOnException = true, bool throwOnNotFound = true);
        /// <summary>
        /// Create a new File and Get the file from the FileSystem asynchronously
        /// </summary>
        /// <param name="fileType">The type of the file</param>
        /// <param name="bytes">The contents of the file</param>
        /// <param name="throwOnException">Indicates whether an Exception should be thrown upon errors. Default:true</param>
        /// <returns>IFileMetaData</returns>
        Task<IFileMetaData> CreateAsync(string fileType, byte[] bytes, bool throwOnException = true);
        /// <summary>
        /// Create a new File and Get the file from the FileSystem synchronously
        /// </summary>
        /// <param name="fileName">Name of the file</param>
        /// <param name="fileType">The type of the file</param>
        /// <param name="bytes">The contents of the file</param>
        /// <param name="throwOnException">Indicates whether an Exception should be thrown upon errors. Default:true</param>
        /// <returns>IFileMetaData</returns>
        IFileMetaData Create(string fileName, string fileType, byte[] bytes, bool throwOnException = true);
        /// <summary>
        /// Create a new File and Get the file from the FileSystem asynchronously
        /// </summary>
        /// <param name="fileName">Name of the file</param>
        /// <param name="fileType">The type of the file</param>
        /// <param name="bytes">The contents of the file</param>
        /// <param name="throwOnException">Indicates whether an Exception should be thrown upon errors. Default:true</param>
        /// <returns>IFileMetaData</returns>
        Task<IFileMetaData> CreateAsync(string fileName, string fileType, byte[] bytes, bool throwOnException = true);
        /// <summary>
        /// Create a new File and Get the file from the FileSystem synchronously
        /// </summary>
        /// <param name="fileType">The type of the file</param>
        /// <param name="bytes">The contents of the file</param>
        /// <param name="throwOnException">Indicates whether an Exception should be thrown upon errors. Default:true</param>
        /// <returns>IFileMetaData</returns>
        IFileMetaData Create(string fileType, byte[] bytes, bool throwOnException = true);
        /// <summary>
        /// Update an already existing File and Get the file from the FileSystem asynchronously
        /// </summary>
        /// <param name="fileName">Name of the file (with extension). Eg: file.zip</param>
        /// <param name="fileType">The Type of the file</param>
        /// <param name="bytes">Data to be written into the file</param>
        /// <param name="throwOnException">Indicates whether an Exception should be thrown upon errors. Default:true</param>
        /// <param name = "throwOnNotFound" > Indicates whether an Exeption should be thrown if the file is not found.Default:true</param>
        /// <returns>IFileMetaData</returns>
        Task<IFileMetaData> UpdateAsync(string fileName, string fileType, byte[] bytes, bool throwOnException = true, bool throwOnNotFound = true);
        /// <summary>
        /// Update an already existing File and Get the file from the FileSystem synchronously
        /// </summary>
        /// <param name="fileName">Name of the file</param>
        /// <param name="fileType">The Type of the file</param>
        /// <param name="bytes">Data to be written into the file</param>
        /// <param name="throwOnException">Indicates whether an Exception should be thrown upon errors. Default:true</param>
        ///<param name = "throwOnNotFound" > Indicates whether an Exeption should be thrown if the file is not found.Default:true</param>
        /// <returns>IFileMetaData</returns>
        IFileMetaData Update(string fileName, string fileType, byte[] bytes, bool throwOnException = true, bool throwOnNotFound = true);
        /// <summary>
        /// Move a file within root
        /// </summary>
        /// <param name="fileName">Name of the file</param>
        /// <param name="oldFileType">The current Type of the file</param>
        /// <param name="newFileType">The new Type of the file</param>
        /// <param name="throwOnException">Indicates whether an Exception should be thrown upon errors. Default:true</param>
        ///<param name = "throwOnNotFound" > Indicates whether an Exeption should be thrown if the file is not found.Default:true</param>
        /// <returns>IFileMetaData</returns>
        IFileMetaData Move(string fileName, string oldFileType, string newFileType, bool throwOnException = true, bool throwOnNotFound = true);
        /// <summary>
        /// Delete a file from the FileSystem
        /// </summary>
        /// <param name="fileName">Name of the file</param>
        /// <param name="fileType">Type of the file</param>
        /// <param name="throwOnException">Indicates whether an Exception should be thrown upon errors. Default:true</param>
        ///<param name = "throwOnNotFound" > Indicates whether an Exeption should be thrown if the file is not found.Default:false</param>
        /// <returns>IFileMetaData</returns>
        void Delete(string fileName, string fileType, bool throwOnException = true, bool throwOnNotFound = false);
    }
}
