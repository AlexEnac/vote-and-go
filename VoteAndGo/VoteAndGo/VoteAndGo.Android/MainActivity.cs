using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.CurrentActivity;
using TinyIoC;
using XLabs.Platform.Device;
using Tesseract;
using Tesseract.Droid;
using XLabs.Ioc;
using XLabs.Ioc.TinyIOC;
using Microblink.Forms.Droid;
using Android.Content;

namespace VoteAndGo.Droid
{
    [Activity(Label = "VoteAndGo", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, global::Microblink.Forms.Droid.IMicroblinkScannerAndroidHostActivity
    {
        public MicroblinkScannerImplementation currentScannerImplementation;

        public Activity HostActivity => this;
        public int ScanActivityRequestCode => 101;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            var container = TinyIoCContainer.Current;

            base.OnCreate(savedInstanceState);

            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            CrossCurrentActivity.Current.Activity = this;

            container.Register<IDevice>(AndroidDevice.CurrentDevice);
            container.Register<ITesseractApi>((cont, parameters) =>
            {
                return new TesseractApi(ApplicationContext, AssetsDeployment.OncePerInitialization);
            });

            Resolver.SetResolver(new TinyResolver(container));


            MicroblinkScannerFactoryImplementation.AndroidHostActivity = this;
            RequestedOrientation = ScreenOrientation.Portrait;


            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        protected override void OnActivityResult(int requestCode, Android.App.Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            currentScannerImplementation.OnActivityResult(requestCode, resultCode, data);
        }

        public void ScanningStarted(MicroblinkScannerImplementation implementation)
        {
            currentScannerImplementation = implementation;
        }
    }
}