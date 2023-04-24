using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTC.Model
{
    internal class RecursiveDirectoryCopier : IDirectoryCopier
    {
        public void CopyDirectory(string sourcePath, string targetPath)
        {
            DirectoryInfo directory = new(sourcePath);

            if (!directory.Exists)
            {
                throw new IOException("source folder does not exist");
            }

            Directory.CreateDirectory(targetPath);

            foreach (var file in directory.GetFiles())
            {
                string targetFilePath = Path.Combine(targetPath, file.Name);
                file.CopyTo(targetFilePath);
            }

            foreach (var subDirectory in directory.GetDirectories())
            {
                string targetSubDirectory = Path.Combine(targetPath, subDirectory.Name);
                CopyDirectory(subDirectory.FullName, targetSubDirectory);
            }
        }
    }
}
