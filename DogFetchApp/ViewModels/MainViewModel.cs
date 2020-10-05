using DogFetchApp.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows;

namespace DogFetchApp.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        public DelegateCommand<string> ChangeLanguageCommand { get; set; }

        public MainViewModel()
        {
            ChangeLanguageCommand = new DelegateCommand<string>(ChangeLanguage);
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
    }
}
