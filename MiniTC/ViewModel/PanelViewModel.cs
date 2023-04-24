using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MiniTC.Model;
using MiniTC.ViewModel.Base;

namespace MiniTC.ViewModel
{
    internal sealed class PanelViewModel : Base.ViewModel
    {
        private FileSystemElement _currentFile;
        private ICommand _refreshDrives;
        private ObservableCollection<FileSystemElement> _drives;

        public FileSystemElement CurrentFile
        {
            get => _currentFile;
            set
            {
                if (value is null)
                {
                    return;
                }

                _currentFile = value;

                UpdateCurrentDirectoryFiles();
                NotifyPropertyChange(nameof(CurrentFile));
            }
        }

        public void UpdateCurrentDirectoryFiles()
        {
            var path = CurrentFile.FilePath.ClosestDirectory;

            CurrentDirectoryFiles = new(FileDataProvider.GetFileSystemElements(path));

            NotifyPropertyChange(nameof(CurrentDirectoryFiles));
        }

        public ObservableCollection<FileSystemElement> Drives 
        { 
            get => _drives;
            set
            {
                _drives = value;
                NotifyPropertyChange(nameof(Drives));
            }
        }

        public ObservableCollection<FileSystemElement> CurrentDirectoryFiles { get; set; }

        public FileSystemElement DefaultFile => CurrentDirectoryFiles.FirstOrDefault();

        public PanelViewModel()
        {
            UpdateLogicalDrives();
            CurrentFile = Drives.FirstOrDefault();
        }

        public ICommand RefreshDrives => _refreshDrives ??= new RelayCommand(
                null,
                _ => UpdateLogicalDrives()
            );

        private void UpdateLogicalDrives()
        {
            Drives = new(FileDataProvider.GetDrives());
        }
    }
}
