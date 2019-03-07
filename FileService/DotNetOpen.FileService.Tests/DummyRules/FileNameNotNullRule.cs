using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotNetOpen.FileService.Tests.DummyRules
{
    public class FileNameNotNullRule : IRule<FileNameNullException>
    {
        public string Name => nameof(FileNameNotNullRule);

        public void Execute(IFileServiceConfig fileServiceConfig, Stream inputStream, string fileType, string fileName = null)
        {
            Validate(fileName);
        }

        public void Execute(IFileServiceConfig fileServiceConfig, byte[] inputBytes, string fileType, string fileName = null)
        {
            Validate(fileName);
        }

        private void Validate(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new FileNameNullException("FileName cnnot be null.");
        }
    }

    
    public class FileNameNullException : Exception
    {
        public FileNameNullException(string message) : base(message) { }
        public FileNameNullException(string message, Exception inner) : base(message, inner) { }
    }
}
