using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AdventOfCode.Shared.Helpers
{
    public static class FileHelper
    {
        public static IEnumerable<string> ReadFileLines(Assembly assembly, string name = "input.txt")
        {
            string resourcePath = assembly.GetManifestResourceNames().Single(str => str.EndsWith(name));

            using var stream = assembly.GetManifestResourceStream(resourcePath);
            using var reader = new StreamReader(stream!);

            while (!reader.EndOfStream)
            {
                yield return reader.ReadLine()!;
            }
        }
    }
}