﻿namespace Sales.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Views;    
    using Xamarin.Forms;

    public class MainViewModel
    {
        public ProductsViewModel Products { get; set; }

        public MainViewModel()
        {
            this.Products = new ProductsViewModel();
        }

        public ICommand AddproductCommand
        {
            get
            {
                return new RelayCommand(GoToAddProduct);

            }

        }

        private async void GoToAddProduct()
        {
           await Application.Current.MainPage.Navigation.PushAsync(new AddProductPage());
        }
    }
}
