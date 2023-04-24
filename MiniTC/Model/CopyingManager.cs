using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MiniTC.Model
{
    internal class CopyingManager
    {
        public IDirectoryCopier DirectoryCopier { private get; init; }

        public INameConflictResolver NameConflictResolver { private get; init; }

        public void Copy(FilePath sourcePath, FilePath destinationPath)
        {
            var source = sourcePath.FullPath;

            var filename = Path.GetFileName(source);

            var targetDirectory = destinationPath.ClosestDirectory;
            var target = Path.Combine(targetDirectory, filename);

            target = VerifyFileName(target);
            TryCopyFile(source, target);
            TryCopyDirectory(source, target);
        }

        private void TryCopyDirectory(string source, string target)
        {
            if (!Directory.Exists(source))
            {
                return;
            }

            if (DirectoryCopier is not { })
            {
                throw new IOException("copying directories is not allowed");
            }

            if (source == target || Path.GetDirectoryName(target) == source)
            {
                throw new IOException("you can't do that");
            }

            var result = MessageBox.Show(
                $"Are you sure you want to copy directory {source} to {target}?",
                "",
                MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                DirectoryCopier.CopyDirectory(source, target);
            }
        }

        private static void TryCopyFile(string source, string target)
        {
            if (!File.Exists(source))
            {
                return;
            }

            File.Copy(source, target);
        }

        private string VerifyFileName(string target)
        {
            if (File.Exists(target) || Directory.Exists(target))
            {
                target = ResolveNameConflict(target);
            }

            return target;
        }

        private string ResolveNameConflict(string target)
        {
            if (NameConflictResolver is not { })
            {
                throw new InvalidOperationException($"this path ({target}) already exists");
            }

            FilePath targetPath = new(target);
            target = NameConflictResolver.Resolve(targetPath);
            return target;
        }
    }
}
