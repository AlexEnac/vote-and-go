using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.CurrentActivity;
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

            base.OnCreate(savedInstanceState);

            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            CrossCurrentActivity.Current.Activity = this;

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