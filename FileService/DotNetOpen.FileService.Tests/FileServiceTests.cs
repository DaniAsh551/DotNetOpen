using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace DotNetOpen.FileService.Tests
{
    [TestClass]
    public class FileServiceTests
    {
        private static string _filesPath = Path.Combine(Environment.CurrentDirectory, "Files");
        private static IFileServiceConfig _config;
        private static IFileService _fileService;
        private static string[] _allowedExtensions;

        [TestMethod]
        public void FileServiceConfigValidation()
        {
            var allowedExtensions = new string[] { "bin" };
            var validConfig = _config;
            var invalidConfig = new FileServiceConfig(null, 0.0d, Configuration.FileSizeUnit.KB, null);

            Assert.IsTrue(validConfig.IsValid());
            Assert.IsFalse(invalidConfig.IsValid());
        }

        [TestMethod]
        public void SizeLimitEnforceTest()
        {
            var fileSizeLimit = _config.MaxUploadSize;
            var fileSizeUnit = _config.FileSizeUnit;

            var fileType = _allowedExtensions[0];

            //Create new stream and fill with 0s just below the limit
            var stream = new MemoryStream();
            while (stream.Length < (fileSizeLimit * 1024))
                stream.WriteByte(byte.MinValue);

            var fileMetaData = _fileService.Create(fileType, stream);

            Assert.IsTrue(fileMetaData.GetFileSize(fileSizeUnit) <= fileSizeLimit);

            //Increase the stream size to well over the limit
            while (stream.Length <= ((fileSizeLimit + 10) * 1024))
                stream.WriteByte(byte.MinValue);

            Assert.ThrowsException<FileSizeLimitExceedException>(() => fileMetaData = _fileService.Create(fileType, stream));
            stream.Dispose();
        }

        [TestMethod]
        public void AllowedExtensionsEnforcingTest()
        {
            var fileSizeLimit = _config.MaxUploadSize;
            var fileSizeUnit = _config.FileSizeUnit;

            var fileType = _allowedExtensions[0];

            //Create new stream and add a 0
            var stream = new MemoryStream();
            stream.WriteByte(byte.MinValue);

            //attempt saving with the right file extension
            var fileMetaData = _fileService.Create(Guid.NewGuid().ToString() + ".bin", fileType, stream);

            Assert.IsTrue(File.Exists(fileMetaData.AbsolutePath));

            Assert.ThrowsException<InvalidExtensionException>(() => _fileService.Create(Guid.NewGuid().ToString() + ".dat", fileType, stream));
        }

        [TestMethod]
        public void RulesExecutionTest()
        {
            var stream = new MemoryStream();
            stream.WriteByte(byte.MinValue);

            _config.Rules.AddRule<DummyRules.FileNameNotNullRule>();

            Assert.IsNotNull(_fileService.Create("someFileName","someFileType", stream));
            Assert.ThrowsException<DummyRules.FileNameNullException>(() => _fileService.Create("someFileType", stream));
        }

        [TestMethod]
        public void DuplicateRuleNameTest()
        {
            var rules = _config.Rules;
            rules.AddRule<DummyRules.FileTypeNotNullRule>();

            Assert.ThrowsException<InvalidRuleException>(() => rules.AddRule<DummyRules.FileTypeNotNullRule>());
        }

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            if (!Directory.Exists(_filesPath))
                Directory.CreateDirectory(_filesPath);

            _allowedExtensions = new string[] { "bin" };

            _config = new FileServiceConfig(stream => "application/octet-stream", 5.0d, Configuration.FileSizeUnit.KB, _filesPath, _allowedExtensions);
            _fileService = new FileService(_config);
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            Directory.Delete(_filesPath, true);
        }
    }
}
