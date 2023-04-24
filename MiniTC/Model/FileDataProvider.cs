using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTC.Model
{
    internal class FileDataProvider
    {
        public static List<FileSystemElement> GetFileSystemElements(string path)
        {
            if (!Directory.Exists(path))
            {
                return null;
            }

            var files = Directory.GetFiles(path);
            var subfolders = Directory.GetDirectories(path);

            var combinedCollection = subfolders.Concat(files).Select(x => new FileSystemElement(x));

            List<FileSystemElement> directoryFiles = new(combinedCollection);

            string back = Path.GetDirectoryName(path.TrimEnd('\\'));
            if (!string.IsNullOrEmpty(back))
            {
                directoryFiles.Insert(0, new FileSystemElement(back).OverrideDisplay(".."));
            }

            return directoryFiles;
        }

        public static IEnumerable<FileSystemElement> GetDrives()
        {
            var rawDrivePathsArray = Directory.GetLogicalDrives();
            var fileSystemElementCollection = rawDrivePathsArray.Select(x => new FileSystemElement(x));
            return fileSystemElementCollection;
        }
    }
}
