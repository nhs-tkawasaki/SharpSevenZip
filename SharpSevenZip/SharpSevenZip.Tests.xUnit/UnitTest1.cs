using System.IO;
using Xunit;

namespace SharpSevenZip.Tests.xUnit
{
    public class UnitTest1 : TestBase
    {
        [Fact]
        public void CompressFileTest()
        {
            var compressor = new SharpSevenZipCompressor
            {
                ArchiveFormat = OutArchiveFormat.SevenZip,
                DirectoryStructure = false
            };

            compressor.CompressFiles(TemporaryFile, @"Testdata\7z_LZMA2.7z");
            Assert.True(File.Exists(TemporaryFile));

            using (var extractor = new SharpSevenZipExtractor(TemporaryFile))
            {
                extractor.ExtractArchive(OutputDirectory);
            }

            Assert.True(File.Exists(Path.Combine(OutputDirectory, "7z_LZMA2.7z")));
        }

        [Fact]
        public void ExtractFilesTest()
        {
            using (var extractor = new SharpSevenZipExtractor(@"TestData\multiple_files.7z"))
            {
                for (var i = 0; i < extractor.ArchiveFileData.Count; i++)
                {
                    extractor.ExtractFiles(OutputDirectory, extractor.ArchiveFileData[i].Index);
                }
            }

            Assert.Equal(3, Directory.GetFiles(OutputDirectory).Length);
        }
    }
}
