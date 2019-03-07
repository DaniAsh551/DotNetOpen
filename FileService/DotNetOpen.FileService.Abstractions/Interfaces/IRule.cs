using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotNetOpen.FileService
{
    /// <summary>
    /// A rule which defines whether a given file should be accepted or rejected.
    /// </summary>
    public interface IRule<TException> : IRule where TException : Exception
    {
    }

    /// <summary>
    /// A rule which defines whether a given file should be accepted or rejected.
    /// </summary>
    public interface IRule
    {
        /// <summary>
        /// Name of the rule
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Executes the rule which should throw an exception of type <see cref="TException"/>.
        /// </summary>
        /// <typeparam name="TException">The type of exception which would be thrown in case of failure.</typeparam>
        /// <param name="fileServiceConfig">The file service config to validate against.</param>
        /// <param name="inputStream">The stream which contains the file.</param>
        /// <param name="fileType">The type of file.</param>
        /// <param name="fileName">Name of the file (this might be null at times).</param>
        void Execute(IFileServiceConfig fileServiceConfig, Stream inputStream, string fileType, string fileName = null);
        /// <summary>
        /// Executes the rule which should throw an exception of type <see cref="TException"/>.
        /// </summary>
        /// <typeparam name="TException">The type of exception which would be thrown in case of failure.</typeparam>
        /// <param name="fileServiceConfig">The file service config to validate against.</param>
        /// <param name="inputBytes">The byte array which contains the file.</param>
        /// <param name="fileType">The type of file.</param>
        /// <param name="fileName">Name of the file (this might be null at times).</param>
        void Execute(IFileServiceConfig fileServiceConfig, byte[] inputBytes, string fileType, string fileName = null);
    }
}
