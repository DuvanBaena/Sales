using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Sales
{
    using Sales.Helpers;
    using Sales.ViewModels;
    using Sales.Views;

    public partial class App : Application
    {
        public static NavigationPage Navigator { get; internal set; }

        public App()
        {
            InitializeComponent();

            if (Settings.IsRemembered && !string.IsNullOrEmpty(Settings.Access_token))
            {
                MainViewModel.GetIntance().Products = new ProductsViewModel();
                MainPage = new MasterPage();
            }
            else
            {
                MainViewModel.GetIntance().Login = new LoginViewModel();
                MainPage = new NavigationPage(new LoginPage());
            }

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
