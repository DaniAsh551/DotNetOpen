using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotNetOpen.FileService
{
    /// <summary>
    /// A class to help with the mime type determination of Data Streams.
    /// </summary>
    public class MimeTypeHelper
    {
        /// <summary>
        /// A function which provides MimeType given the Data stream.
        /// </summary>
        /// <param name="stream">Data stream.</param>
        /// <returns>The MIME type of the data within the stream.</returns>
        public delegate string GetMimeTypeDelegate(Stream stream);
    }
}
