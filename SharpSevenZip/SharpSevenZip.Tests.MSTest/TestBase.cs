using System;
using System.IO;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpSevenZip.Tests.MSTest;

public abstract class TestBase
{
    protected const string OutputDirectory = "output";
    protected readonly string TemporaryFile = Path.Combine(OutputDirectory, "tmp.7z");

    [TestInitialize]
    public void SetUp()
    {
        Directory.SetCurrentDirectory(AppContext.BaseDirectory);
        Directory.CreateDirectory(OutputDirectory);
    }

    [TestCleanup]
    public void TearDown()
    {
        for (var n = 0; n < 10; n++)
        {
            try
            {
                if (Directory.Exists(OutputDirectory))
                    Directory.Delete(OutputDirectory, true);
                break;
            }
            catch
            {
                Thread.Sleep(20);
            }
        }
    }
}