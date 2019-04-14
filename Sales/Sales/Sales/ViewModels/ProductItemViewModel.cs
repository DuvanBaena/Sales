

namespace Sales.ViewModels
{
    using System.Linq;
    using System.Windows.Input;
    using Common.Models;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Services;
    using Xamarin.Forms;

    public class ProductItemViewModel   : Product
    {
        #region Attributes
        private ApiService apiService;
        #endregion

        #region Constructors
        public ProductItemViewModel()
        {

            this.apiService = new ApiService();
        }
        #endregion

        #region Command
        public ICommand DeleteProductCommand
        {
            get
            {
                return new RelayCommand(DeleteProduct);
            }
            
        }

        private async void DeleteProduct()
        {
            var answer = await  Application.Current.MainPage.DisplayAlert(
                Languages.Confirm,
                Languages.DeleteConfirmation,
                Languages.Yes,
                Languages.No);

            if (!answer)
            {
                return;            
            }

            var conecction = await this.apiService.CheckConnection();
            if (!conecction.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, conecction.Message, Languages.Accept);
                return;
            }

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProductController"].ToString();
            var response = await this.apiService.Delete(url, prefix, controller, this.ProductId);
            if (!response.IsSuccess)
            {               
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error, 
                    response.Message, 
                    Languages.Accept);
                return;
            }

            var productsViewModel = ProductsViewModel.GetIntance();
            var deletedProduct = productsViewModel.Products.Where(p => p.ProductId == this.ProductId).FirstOrDefault();
            if (deletedProduct != null)
            {
                productsViewModel.Products.Remove(deletedProduct);
            }
        }



        #endregion
    }
}
