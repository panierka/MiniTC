using System;
using System.Windows;
using System.Windows.Input;
using MiniTC.Model;
using MiniTC.ViewModel.Base;

namespace MiniTC.ViewModel
{
    internal sealed class MainViewModel : Base.ViewModel
    {
        private ICommand _copy;

        public PanelViewModel PanelAViewModel { get; set; } = new();
        public PanelViewModel PanelBViewModel { get; set; } = new();

        private CopyingManager CopyingManager { get; init; }

        public MainViewModel()
        {
            CopyingManager = new()
            {
                DirectoryCopier = new RecursiveDirectoryCopier(),
                NameConflictResolver = new NumberingNameConflictResolver(),
            };
        }

        public ICommand Copy => _copy ??= new RelayCommand(
            null,
            _ =>
            {
                try
                {
                    var sourcePath = PanelAViewModel.CurrentFile.FilePath;
                    var targetPath = PanelBViewModel.CurrentFile.FilePath;

                    CopyingManager.Copy(sourcePath, targetPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                PanelBViewModel.UpdateCurrentDirectoryFiles();
            });
    }
}
