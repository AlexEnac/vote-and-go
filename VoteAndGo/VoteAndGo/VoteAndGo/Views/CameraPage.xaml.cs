using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoteAndGo.Models;
using VoteAndGo.ViewModels;
using VoteAndGo.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VoteAndGo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraPage : ContentPage
    {
        CameraViewModel _viewModel;
        public CameraPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new CameraViewModel();

            CameraButton.Clicked += CameraButton_Clicked;
        }

        private async void CameraButton_Clicked(object sender, EventArgs e)
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });

            if (photo != null)
                _viewModel.fillImage(photo.GetStream());
        }
    }
}