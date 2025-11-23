using System;
using System.IO;
using System.Threading;

namespace SharpSevenZip.Tests.xUnit
{

    public abstract class TestBase : IDisposable
    {
        protected const string OutputDirectory = "output";
        protected readonly string TemporaryFile = Path.Combine(OutputDirectory, "tmp.7z");

        protected TestBase()
        {
            Directory.SetCurrentDirectory(AppContext.BaseDirectory);
            Directory.CreateDirectory(OutputDirectory);
        }

        public void Dispose()
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
}