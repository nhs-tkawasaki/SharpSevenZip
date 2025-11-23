using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpSevenZip.Tests.MSTest
{
    [TestClass]
    public sealed class Test1 : TestBase
    {
        [TestMethod]
        public void CompressFileTest()
        {
            var compressor = new SharpSevenZipCompressor
            {
                ArchiveFormat = OutArchiveFormat.SevenZip,
                DirectoryStructure = false
            };

            compressor.CompressFiles(TemporaryFile, @"Testdata\7z_LZMA2.7z");
            Assert.IsTrue(File.Exists(TemporaryFile));

            using (var extractor = new SharpSevenZipExtractor(TemporaryFile))
            {
                extractor.ExtractArchive(OutputDirectory);
            }

            Assert.IsTrue(File.Exists(Path.Combine(OutputDirectory, "7z_LZMA2.7z")));
        }

        [TestMethod]
        public void ExtractFilesTest()
        {
            using (var extractor = new SharpSevenZipExtractor(@"TestData\multiple_files.7z"))
            {
                for (var i = 0; i < extractor.ArchiveFileData.Count; i++)
                {
                    extractor.ExtractFiles(OutputDirectory, extractor.ArchiveFileData[i].Index);
                }
            }

            Assert.HasCount(3, Directory.GetFiles(OutputDirectory));
        }
    }
}
