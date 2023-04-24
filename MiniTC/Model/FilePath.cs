using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTC.Model
{
    internal class FilePath
    {
        private readonly string[] components;

        public FilePath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path), "path can not be empty nor null");
            }

            if (!File.Exists(path) && !Directory.Exists(path))
            {
                throw new IOException($"path {path} is invalid");
            }

            components = path.Split('\\');
        }

        public string FullPath => Path.Combine(components);

        public string ClosestDirectory
        {
            get
            {
                if (Directory.Exists(FullPath))
                {
                    return FullPath + "\\";
                }

                return Path.Combine(components[..^1]) + "\\";
            }
        }

        public string FileName => components[^1];

        public string PathWithNoExtension
        {
            get
            {
                if (Directory.Exists(FullPath))
                {
                    return FullPath;
                }

                return FullPath[..FullPath.LastIndexOf('.')];
            }
        }

        public string Extension
        {
            get
            {
                if (Directory.Exists(FullPath))
                {
                    return string.Empty;
                }

                return FullPath[FullPath.LastIndexOf('.')..];
            }
        }
    }
}
