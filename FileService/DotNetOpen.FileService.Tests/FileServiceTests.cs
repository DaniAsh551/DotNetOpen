using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace DotNetOpen.FileService.Tests
{
    [TestClass]
    public class FileServiceTests
    {
        [TestMethod]
        public void FileServiceConfigValidation()
        {
            var filesPath = Path.Combine(Environment.CurrentDirectory, "Files");
            if(!Directory.Exists(filesPath))
                Directory.CreateDirectory(filesPath);
            var allowedExtensions = new string[] { "bin" };
            var validConfig = new FileServiceConfig(stream => "application/octet-stream", 5.0d, Configuration.FileSizeUnit.KB, filesPath, allowedExtensions);
            var invalidConfig = new FileServiceConfig(null, 0.0d, Configuration.FileSizeUnit.KB, null, null);

            Assert.IsTrue(validConfig.IsValid());
            Assert.IsFalse(invalidConfig.IsValid());

            Directory.Delete(filesPath, true);
        }

        [TestMethod]
        public void FileServiceSizeLimitEnforceTest()
        {
            var filesPath = Path.Combine(Environment.CurrentDirectory, "Files");
            if (!Directory.Exists(filesPath))
                Directory.CreateDirectory(filesPath);

            var fileSizeLimit = 5.0d;
            var fileSizeUnit = Configuration.FileSizeUnit.KB;

            var fileType = "bin";

            var allowedExtensions = new string[] { fileType };
            var config = new FileServiceConfig(s => "application/octet-stream", fileSizeLimit, fileSizeUnit, filesPath, allowedExtensions);

            var fileService = new FileService(config);

            //Create new stream and fill with 0s just below the limit
            var stream = new MemoryStream();
            while (stream.Length < (fileSizeLimit * 1024))
                stream.WriteByte(byte.MinValue);

            var fileMetaData = fileService.Create(fileType, stream);

            Assert.IsTrue(fileMetaData.GetFileSize(fileSizeUnit) <= fileSizeLimit);

            //Increase the stream size to well over the limit
            while (stream.Length <= ((fileSizeLimit + 10) * 1024))
                stream.WriteByte(byte.MinValue);

            Assert.ThrowsException<FileSizeLimitExceedException>(() => fileMetaData = fileService.Create(fileType, stream));
            stream.Dispose();

            Directory.Delete(filesPath, true);
        }
    }
}
