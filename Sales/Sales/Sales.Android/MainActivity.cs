
namespace Sales.Droid
{
    using System;
    using Android.App;
    using Android.Content.PM;
    using Android.Runtime;
    using Android.Views;
    using Android.Widget;
    using Android.OS;
    using Plugin.Permissions;
    using Plugin.CurrentActivity;
    using ImageCircle.Forms.Plugin.Droid;

    [Activity(
        Label = "Sales",
        Icon = "@mipmap/icon",
        Theme = "@style/MainTheme",
        MainLauncher = true, 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            ImageCircleRenderer.Init();
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        //public override void OnRequestPermissionsResult(
        //    int requestCode,
        //    string[] permissions, 
        //    [GeneratedEnum] Permission[] grantResults)
        //{
        //    PermissionsImplementation.Current.OnRequestPermissionsResult(
        //        requestCode, 
        //        permissions, 
        //        grantResults);
        //}

        public override void OnRequestPermissionsResult(
            int requestCode,
            string[] permissions, 
            Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(
                requestCode,
                permissions, 
                grantResults);
        }
    }
}