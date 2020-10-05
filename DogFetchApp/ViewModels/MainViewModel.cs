using ApiHelper;
using DogFetchApp.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows;

namespace DogFetchApp.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private string _selectedBreed;
        public string SelectedBreed
        {
            get => _selectedBreed;
            set
            {
                _selectedBreed = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> BreedList { get; set; }

        public DelegateCommand<string> ChangeLanguageCommand { get; set; }

        public MainViewModel()
        {
            ChangeLanguageCommand = new DelegateCommand<string>(ChangeLanguage);
            BreedList = new ObservableCollection<string>();

            ApiHelper.ApiHelper.InitializeClient();
            UpdateBreedList();
        }

        internal void ChangeLanguage(string lang)
        {
            if (Properties.Settings.Default.Language == lang)
            {
                return;
            }

            Properties.Settings.Default.Language = lang;
            Properties.Settings.Default.Save();
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);

            var res = MessageBox.Show("Changing languages requires an application restart. Do you want to restart now?", "Restart Application", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                Application.Current.Shutdown();
            }
        }

        private async void UpdateBreedList()
        {
            BreedList.Clear();

            var breeds = await DogApiProcessor.LoadBreedList();
            foreach (var breed in breeds)
            {
                BreedList.Add(breed);
            }

            if (BreedList.Count > 0)
            {
                SelectedBreed = BreedList[0];
            }
        }
    }
}
