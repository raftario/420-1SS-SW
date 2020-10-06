using ApiHelper;
using DogFetchApp.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        private int _selectedCount = 5;
        public int SelectedCount
        {
            get => _selectedCount;
            set
            {
                _selectedCount = value;
                OnPropertyChanged();
            }
        }

        private bool _breedsLoaded = false;
        public bool BreedsLoaded
        {
            get => _breedsLoaded;
            set
            {
                _breedsLoaded = value;
                OnPropertyChanged();
            }
        }

        private bool _nextEnabled = false;
        public bool NextEnabled
        {
            get => _nextEnabled;
            set
            {
                _nextEnabled = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> BreedList { get; set; }
        public ObservableCollection<int> CountList { get; set; }
        public ObservableCollection<string> DogImages { get; set; }

        public DelegateCommand<string> ChangeLanguageCommand { get; set; }
        public AsyncCommand<object> FetchDogsCommand { get; set; }

        public MainViewModel()
        {
            ChangeLanguageCommand = new DelegateCommand<string>(ChangeLanguage);
            FetchDogsCommand = new AsyncCommand<object>(FetchDogs);
            BreedList = new ObservableCollection<string>();
            CountList = new ObservableCollection<int> { 1, 2, 3, 5, 10 };
            DogImages = new ObservableCollection<string>();

            ApiHelper.ApiHelper.InitializeClient();
            InitBreedList();
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

        private async void InitBreedList()
        {
            var breeds = await DogApiProcessor.LoadBreedList();
            BreedList.Clear();
            foreach (var breed in breeds)
            {
                BreedList.Add(breed);
            }

            if (BreedList.Count > 0)
            {
                SelectedBreed = BreedList[0];
                BreedsLoaded = true;
            }
        }

        private async Task FetchDogs(object _)
        {
            var imageUrls = await Task.WhenAll(Enumerable.Range(0, SelectedCount).Select(_ => DogApiProcessor.GetImageUrl(SelectedBreed)));
            DogImages.Clear();
            foreach (var url in imageUrls)
            {
                DogImages.Add(url);
            }

            NextEnabled = true;
        }
    }
}
