﻿
namespace Sales.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Helpers;
    using Services;
    using Sales.Views;
    using System;

    public class LoginViewModel : BaseViewModel
    {
        #region Attribute
        private ApiService apiService; 
        #endregion

        #region Properties
        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsRemembered { get; set; }
        
        private bool isRunning;

        private bool isEnabled;

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { this.SetValue(ref this.isRunning, value); }
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { this.SetValue(ref this.isEnabled, value); }
        }
        #endregion

        #region Constructor
        public LoginViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.IsRemembered = true;
            
        }
        #endregion

        #region Command
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
           
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailValidation,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PasswordValidation,
                    Languages.Accept);
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var conecction = await this.apiService.CheckConnection();
            if (!conecction.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, conecction.Message, Languages.Accept);
                return;
            }
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var token = await this.apiService.GetToken(url, this.Email, this.Password);

            if (token == null || string.IsNullOrEmpty(token.AccessToken))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.SomethingWrong, 
                    Languages.Accept);
                return;
            }

            Settings.Token_type = token.TokenType;
            Settings.Access_token = token.AccessToken;
            Settings.IsRemembered = this.IsRemembered;

            Application.Current.MainPage = new ProductsPage();
            MainViewModel.GetIntance().Products = new ProductsViewModel();

            this.IsRunning = false;
            this.IsEnabled = true;
        }


        #endregion
    }
}
