﻿
namespace Sales.ViewModels
{   

    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;
    using Helpers;
    using Views;

    public class MenuItemViewModel
    {
        #region Properties
        public string Icon { get; set; }

        public string Title { get; set; }

        public string PageName { get; set; }
        #endregion

        #region Commands
        public ICommand GotoCommand
        {
            get
            {
                return new RelayCommand(Goto);
            }
        }

        private void Goto()
        {
            if (this.PageName == "LoginPage")
            {
                Settings.Access_token = string.Empty;
                Settings.Token_type = string.Empty;
                Settings.IsRemembered = false;
                MainViewModel.GetIntance().Login = new LoginViewModel();
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
        }



        #endregion
    }
}
