using FlaUI.UIA3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MetBench_Test
{
    public class AbsolutePathResolver
    {

        private static readonly Lazy<string> exepathLazy = new Lazy<string>(InitializeExepath);

        private static string InitializeExepath()
        {
            string solutionPath = Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            string parentDirectory = Directory.GetParent(solutionPath).Parent.FullName;
            return Path.Combine(parentDirectory, "MetBench_Client", "bin", "Debug", "net6.0-windows", "MetBench_Client.exe");
        }

        public static string GetSolutionPath()
        {
            return exepathLazy.Value;
        }
    }
}
