using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;
using VoteAndGo.Models;
using VoteAndGo.ViewModels;
using VoteAndGo.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Ioc;
using XLabs.Platform.Services.Media;

namespace VoteAndGo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraPage : ContentPage
    {
        CameraViewModel _viewModel;
        private readonly ITesseractApi _tesseractApi;

        public CameraPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new CameraViewModel();
            _tesseractApi = Resolver.Resolve<ITesseractApi>();

            CameraButton.Clicked += CameraButton_Clicked;
        }

        private async void CameraButton_Clicked(object sender, EventArgs e)
        {
            if (!_tesseractApi.Initialized)
                await _tesseractApi.Init("ron");

            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });

            if (photo != null)
            {
                var photoStream = photo.GetStream();
                var imageBytes = new byte[photoStream.Length];
                photoStream.Position = 0;
                photoStream.Read(imageBytes, 0, (int)photoStream.Length);
                photoStream.Position = 0;

                _viewModel.fillImage(photoStream);
                var tessResult = await _tesseractApi.SetImage(imageBytes);
                if (tessResult)
                {
                    RecognizedText.Text = _tesseractApi.Text;
                }
            }
        }

    }
}