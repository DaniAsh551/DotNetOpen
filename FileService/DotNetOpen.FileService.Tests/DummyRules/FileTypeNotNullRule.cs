using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotNetOpen.FileService.Tests.DummyRules
{
    public class FileTypeNotNullRule : IRule<FileTypeNullException>
    {
        public string Name => nameof(FileTypeNotNullRule);

        public void Execute(IFileServiceConfig fileServiceConfig, Stream inputStream, string fileType, string fileName = null)
        {
            Validate(fileType);
        }

        public void Execute(IFileServiceConfig fileServiceConfig, byte[] inputBytes, string fileType, string fileName = null)
        {
            Validate(fileType);
        }

        private void Validate(string fileType)
        {
            if (string.IsNullOrEmpty(fileType))
                throw new FileTypeNullException("FileType cannot be null.");
        }
    }


    public class FileTypeNullException : Exception
    {
        public FileTypeNullException() { }
        public FileTypeNullException(string message) : base(message) { }
        public FileTypeNullException(string message, Exception inner) : base(message, inner) { }
    }
}
