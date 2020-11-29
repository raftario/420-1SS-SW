using System;
using System.Collections.Generic;
using System.Linq;
using WeatherApp.Commands;
using WeatherApp.Services;

namespace WeatherApp.ViewModels
{
    public class ApplicationViewModel : BaseViewModel
    {
        #region Membres
        private BaseViewModel currentViewModel;
        private List<BaseViewModel> viewModels;
        private OpenWeatherService ows;

        #endregion

        #region Propriétés
        /// <summary>
        /// Model actuellement affiché
        /// </summary>
        public BaseViewModel CurrentViewModel
        {
            get { return currentViewModel; }
            set { 
                currentViewModel = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Commande pour changer la page à afficher
        /// </summary>
        public DelegateCommand<string> ChangePageCommand { get; set; }

        public List<BaseViewModel> ViewModels
        {
            get {
                if (viewModels == null)
                    viewModels = new List<BaseViewModel>();
                return viewModels; 
            }
        }
        #endregion

        public ApplicationViewModel()
        {
            ChangePageCommand = new DelegateCommand<string>(ChangePage);
           
            // var apiKey = AppConfiguration.GetValue("OWApiKey");
            ows = new OpenWeatherService(Properties.Settings.Default.apiKey);

            initViewModels();
        }

        #region Méthodes
        void initViewModels()
        {
            /// TemperatureViewModel setup
            var tvm = new TemperatureViewModel();
            tvm.SetTemperatureService(ows);
            ViewModels.Add(tvm);

            var cvm = new ConfigurationViewModel();
            ViewModels.Add(cvm);

            CurrentViewModel = ViewModels[0];
        }

        private void ChangePage(string pageName)
        {
            if (currentViewModel.Name == nameof(ConfigurationViewModel) && !string.IsNullOrEmpty(Properties.Settings.Default.apiKey))
            {
                ows.SetApiKey(Properties.Settings.Default.apiKey);

                var tvm = (TemperatureViewModel) ViewModels[0];
                if (tvm.TemperatureService is null)
                {
                    tvm.SetTemperatureService(ows);
                }
            }

            /// Permet de retrouver le ViewModel avec le nom indiqé
            CurrentViewModel = ViewModels.FirstOrDefault(x => x.Name == pageName);  
        }

        #endregion
    }
}
