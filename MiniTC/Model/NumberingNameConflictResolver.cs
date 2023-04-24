using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTC.Model
{
    internal class NumberingNameConflictResolver : INameConflictResolver
    {
        public string Resolve(FilePath path)
        {
            return RecursivelyNumberName(path, 1);
        }

        private string RecursivelyNumberName(FilePath path, int number)
        {
            string newPath = $"{path.PathWithNoExtension}-{number}{path.Extension}";

            if (File.Exists(newPath))
            {
                return RecursivelyNumberName(path, ++number);
            }

            if (Directory.Exists(newPath))
            {
                return RecursivelyNumberName(path, ++number);
            }

            return newPath;
        }
    }
}
