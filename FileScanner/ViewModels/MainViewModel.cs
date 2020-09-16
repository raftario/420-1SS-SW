using FileScanner.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileScanner.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public class FolderItem
        {
            public string Image { get; set; }
            public string Text { get; set; }
        }

        private string selectedFolder;
        private ObservableCollection<FolderItem> folderItems = new ObservableCollection<FolderItem>();
         
        public DelegateCommand<string> OpenFolderCommand { get; private set; }
        public AsyncDelegateCommand<string> ScanFolderCommand { get; private set; }

        public ObservableCollection<FolderItem> FolderItems { 
            get => folderItems;
            set 
            { 
                folderItems = value;
                OnPropertyChanged();
            }
        }

        public string SelectedFolder
        {
            get => selectedFolder;
            set
            {
                selectedFolder = value;
                OnPropertyChanged();
                ScanFolderCommand.RaiseCanExecuteChanged();
            }
        }

        public MainViewModel()
        {
            OpenFolderCommand = new DelegateCommand<string>(OpenFolder);
            ScanFolderCommand = new AsyncDelegateCommand<string>(ScanFolder, CanExecuteScanFolder);
        }

        private bool CanExecuteScanFolder(string obj)
        {
            return !string.IsNullOrEmpty(SelectedFolder);
        }

        private void OpenFolder(string obj)
        {
            using var fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                SelectedFolder = fbd.SelectedPath;
            }
        }

        private async Task ScanFolder(string dir)
        {
            var list = await Task.Run(() =>
            {
                var list = new List<FolderItem>();

                try
                {
                    foreach (var d in Directory.EnumerateDirectories(dir, "*"))
                    {
                        list.Add(new FolderItem { Image = "/Assets/folder.png", Text = d });
                    }
                    foreach (var f in Directory.EnumerateFiles(dir, "*"))
                    {
                        list.Add(new FolderItem { Image = "/Assets/file.png", Text = f });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Permission error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return list;
            });

            FolderItems = new ObservableCollection<FolderItem>(list);
        }


        ///TODO : Tester avec un dossier avec beaucoup de fichier
        ///TODO : Rendre l'application asynchrone
        ///TODO : Ajouter un try/catch pour les dossiers sans permission


    }
}
