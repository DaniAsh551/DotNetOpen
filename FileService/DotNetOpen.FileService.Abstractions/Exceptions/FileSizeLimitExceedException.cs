using DotNetOpen.FileService.Configuration;
using System;

namespace DotNetOpen.FileService
{
    public class FileSizeLimitExceedException : Exception
    {
        public readonly long MaxAllowedFileSize;
        public readonly long AttemptedFileSize;
        public readonly FileSizeUnit FileSizeUnit;
        
        public FileSizeLimitExceedException(FileSizeUnit fileSizeUnit, long maxAllowedFileSize, long attemptedFileSize, string message) : base(message)
        {
            FileSizeUnit = fileSizeUnit;
            MaxAllowedFileSize = maxAllowedFileSize;
            AttemptedFileSize = attemptedFileSize;
        }
        public FileSizeLimitExceedException(FileSizeUnit fileSizeUnit, long maxAllowedFileSize, long attemptedFileSize, string message, Exception inner) : base(message, inner)
        {
            FileSizeUnit = fileSizeUnit;
            MaxAllowedFileSize = maxAllowedFileSize;
            AttemptedFileSize = attemptedFileSize;
        }
        protected FileSizeLimitExceedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public FileSizeLimitExceedException(FileSizeUnit fileSizeUnit, long maxAllowedFileSize, long attemptedFileSize) : base("The File size exceeds the Maximum Allowed file size set by the Configuration.")
        {
            FileSizeUnit = fileSizeUnit;
            MaxAllowedFileSize = maxAllowedFileSize;
            AttemptedFileSize = attemptedFileSize;
        }
    }
}
