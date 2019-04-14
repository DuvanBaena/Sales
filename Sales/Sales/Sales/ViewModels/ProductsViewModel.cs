namespace Sales.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;    
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;
    using Helpers;
    using Services;
    using Sales.Common.Models;

    public class ProductsViewModel  : BaseViewModel
    {
        #region Attributes
        private ApiService apiService;

        private bool isRefreshing;
        #endregion

        #region Properties
        private ObservableCollection<Product> products;

        public ObservableCollection<Product> Products
        {
            get { return this.products; }
            set { this.SetValue(ref this.products, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }
        #endregion


        #region Constructure
        public ProductsViewModel()
        {
            instance = this;
            this.apiService = new ApiService();
            this.LoadProducts();
        }
        #endregion

        #region Singleton

        private static ProductsViewModel instance;

        public static ProductsViewModel GetIntance()
        {

            if (instance == null)
            {
                return new ProductsViewModel();
            }

            return instance;

        }
            

        #endregion


        #region Methods
        private async void LoadProducts()
        {
            this.IsRefreshing = true;

            var conecction = await this.apiService.CheckConnection();
            if (!conecction.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, conecction.Message, Languages.Accept);
                return;
            }
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProductController"].ToString();
            var response = await this.apiService.GetList<Product>(url, prefix, controller);
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            var list = (List<Product>)response.Result;
            this.Products = new ObservableCollection<Product>(list);
            this.IsRefreshing = false;
        }
        #endregion


        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadProducts);

            }

        } 
        #endregion
    }
}
