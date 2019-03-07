using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetOpen.FileService
{
    public class InvalidExtensionException : Exception
    {
        public readonly string AttemptedExtension;
        public readonly string[] AllowedExtensions;
        public InvalidExtensionException(string attemptedExtension, string[] allowedExtensions) : base($"The extension '{attemptedExtension}' is not given as an allowed extension in the Configuration.")
        {
            AttemptedExtension = attemptedExtension;
            AllowedExtensions = allowedExtensions;
        }
        public InvalidExtensionException(string attemptedExtension, string[] allowedExtensions, string message) : base(message)
        {
            AttemptedExtension = attemptedExtension;
            AllowedExtensions = allowedExtensions;
        }
        public InvalidExtensionException(string attemptedExtension, string[] allowedExtensions, string message, Exception inner) : base(message, inner)
        {
            AttemptedExtension = attemptedExtension;
            AllowedExtensions = allowedExtensions;
        }
        protected InvalidExtensionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
