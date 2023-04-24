using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace MiniTC.Model
{
    internal sealed class FileSystemElement
    {
        public FilePath FilePath { get; }

        public Type FileType { get; } = Type.FILE;

        private string fixedDisplay = null;

        public FileSystemElement(string path)
        {
            FilePath = new(path);

            if (Directory.Exists(FilePath.FullPath))
            {
                FileType = Type.DIRECTORY;
            } 
        }

        public FileSystemElement OverrideDisplay(string fixedDisplay)
        {
            this.fixedDisplay = fixedDisplay;
            return this;
        }

        public override string ToString()
        {
            return FilePath.FullPath;
        }

        public string Display
        {
            get
            {
                if (!string.IsNullOrEmpty(fixedDisplay))
                {
                    return fixedDisplay;
                }

                string display = Path.GetFileName(FilePath.FullPath);

                if (FileType is Type.DIRECTORY)
                {
                    return $"<D>{display}";
                }

                return display;
            }
        }


        public enum Type : int
        {
            FILE,
            DIRECTORY
        }
    }
}
