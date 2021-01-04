using Xamarin.Forms;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;

namespace VoteAndGo.ViewModels
{
    public class CameraViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public CameraViewModel()
        {
            Title = "Camera";
            image = ImageSource.FromFile("xamagon.png");
        }

        internal void fillImage(Stream stream)
        {
            photoStream = stream;
            Image = ImageSource.FromStream(() => { return photoStream; });
        }

        public ImageSource Image
        {
            get
            {
                return image;
            }

            set
            {
                image = value;

                var args = new PropertyChangedEventArgs(nameof(Image));

                PropertyChanged?.Invoke(this, args);
            }
        }

        private ImageSource image;
        private Stream photoStream;

        #region Property Changed

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        /*
    public async Task<ImageSource> TakePhoto()
    {
        if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
        {
            await App.Current.MainPage.DisplayAlert("No Camera","Sorry! No camera available.", "OK");
            return null;
        }

        var isPermissionGranted = await RequestCameraAndGalleryPermissions();
        if (!isPermissionGranted)
            return null;

        var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
        {
            Directory = "TestPhotoFolder",
            SaveToAlbum = true,
            PhotoSize = PhotoSize.Medium,
        });

        if (file == null)
            return null;

        var imageSource = ImageSource.FromStream(() =>
        {
            var stream = file.GetStream();
            return stream;
        });

        return imageSource;
    }


    private async Task<bool> RequestCameraAndGalleryPermissions()
    {
        var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
        var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.StorageRead);
        var photosStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Photos);

        if (
        cameraStatus != PermissionStatus.Granted ||
        storageStatus != PermissionStatus.Granted ||
        photosStatus != PermissionStatus.Granted)
        {
            var permissionRequestResult = await CrossPermissions.Current.RequestPermissionsAsync(new Permission[]
            {
                Permission.Camera,
                Permission.StorageRead,
                Permission.Photos
            });

            var cameraResult = permissionRequestResult[Permission.Camera];
            var storageResult = permissionRequestResult[Permission.Storage];
            var photosResults = permissionRequestResult[Permission.Photos];

            return (
                cameraResult != PermissionStatus.Denied &&
                storageResult != PermissionStatus.Denied &&
                photosResults != PermissionStatus.Denied);
        }

        return true;
    }*/
    }
}
