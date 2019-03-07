using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotNetOpen.FileService
{
    public interface IRuleContainer : IEnumerable<IRule>
    {
        /// <summary>
        /// The number of rules in the container.
        /// </summary>
        long Length { get; }
        /// <summary>
        /// Add a rule into the container.
        /// </summary>
        /// <param name="rule">The rule to be added.</param>
        /// <exception cref="InvalidRuleException">Thrown when the rule is invalid.</exception>
        void AddRule<TRule>() where TRule : IRule;
        /// <summary>
        /// Executes all the rules in the container on the given file in accordance with the given <seealso cref="IFileServiceConfig"/>.
        /// </summary>
        /// <param name="fileServiceConfig">The <see cref="IFileService"/> against which the files should be validated.</param>
        /// <param name="inputStream">The stream which contains the file.</param>
        /// <param name="fileType">The type of file.</param>
        /// <param name="fileName">The name of the file (This may be null at times).</param>
        void ExecuteAllRules(IFileServiceConfig fileServiceConfig, Stream inputStream, string fileType, string fileName = null);
        /// <summary>
        /// Executes all the rules in the container on the given file in accordance with the given <seealso cref="IFileServiceConfig"/>.
        /// </summary>
        /// <param name="fileServiceConfig">The <see cref="IFileService"/> against which the files should be validated.</param>
        /// <param name="inputBytes">The byte array which contains the file.</param>
        /// <param name="fileType">The type of file.</param>
        /// <param name="fileName">The name of the file (This may be null at times).</param>
        void ExecuteAllRules(IFileServiceConfig fileServiceConfig, byte[] inputBytes, string fileType, string fileName = null);
    }
}
