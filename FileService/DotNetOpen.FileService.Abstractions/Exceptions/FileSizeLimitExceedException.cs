using System;

namespace DotNetOpen.FileService
{
    public class FileSizeLimitExceedException : Exception
    {
        public long MaxAllowedFileSize { get; set; }
        public long AttemptedFileSize { get; set; }

        public FileSizeLimitExceedException(long maxAllowedFileSize, long attemptedFileSize) : base("The File size exceeds the Maximum Allowed file size set by the Configuration.")
        {
            MaxAllowedFileSize = maxAllowedFileSize;
            AttemptedFileSize = attemptedFileSize;
        }
    }
}
