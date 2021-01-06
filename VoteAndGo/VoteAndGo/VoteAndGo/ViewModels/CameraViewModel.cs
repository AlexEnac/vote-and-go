using Xamarin.Forms;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;

using Microblink.Forms.Core;
using Microblink.Forms.Core.Overlays;
using Microblink.Forms.Core.Recognizers;

namespace VoteAndGo.ViewModels
{
    public class CameraViewModel : BaseViewModel
    {
        IMicroblinkScanner blinkID;
        IBlinkIdCombinedRecognizer blinkidRecognizer;

        #region Constructor
        public CameraViewModel()
        {
            Title = "Camera";
            var microblinkFactory = DependencyService.Get<IMicroblinkScannerFactory>();

            string licenseKey;
            if (Device.RuntimePlatform == Device.iOS)
            {
                licenseKey = "sRwAAAEZY29tLmNvbXBhbnluYW1lLnZvdGVhbmRnb6t9OlpDWCWMDqzJirFaSFElbPZnaKjIBx+5RpERL6KXJjAPlg/JAkqR1NjJbzHN5+2pX60wQCW2MALlwWd34x4IBv8zZKE7ra0yO9U0Gby8PY2mHQZ57/KEXkWT9fjAUhLojbR3nFIEmIbLEVZXYm1Q9e3tRzV6jhC9UHShjhauR/0y/cbuclITlIieaUbK+DR/8ZB4DVQig+C5wNM60AWdeCkegXPh9dw+XuDOcuvW/qIhTRpS+5+hK/lUyuHDFcxS1ws3ls0arN2mY87Sh7kXzfyVHVISgEfRPSj1To7zj7M7SNLWWjfvAAZcyuz8Uf7Z3AutUg==";
            }
            else
            {
                licenseKey = "sRwAAAAZY29tLmNvbXBhbnluYW1lLnZvdGVhbmRnbyVK+xqLu6s55kBlGozGRrfKhHkSnRSWHa62QdikC2p2CEzT3Ca8PanJIaTrFxS9D9RhaL1qIuQsxgpbJaW5kkydXNRBbO1+gzwKqMv9NOfYzLjTjTujUQP7R4+ar27At+tBU/DyDcYhECXgzsxibdvL4qG/jhbQpo6ewq7NlWftpmH9yMbjubgmNSapkY/7E9VPlSyFqWz4KpN0+TqD6ppNlPq4S9cTIxC9lUwVrZs0Q6hjBOf/Q74oORfmqYuuyhoV+rQyLIOqY5dw52gX/XM96lLxk9pI2lUhvwRvGQZ/8RukzrYWqGv94MV+m1gvcgELK8wf6Q==";
            }

            blinkID = microblinkFactory.CreateMicroblinkScanner(licenseKey);
            MessagingCenter.Subscribe<Messages.ScanningDoneMessage>(this, Messages.ScanningDoneMessageId, (sender) => {
                ImageSource faceImageSource = null;
                ImageSource fullDocumentFrontImageSource = null;
                ImageSource fullDocumentBackImageSource = null;
                ImageSource successFrameImageSource = null;

                string stringResult = "No valid results.";

                if (sender.ScanningCancelled)
                {
                    stringResult = "Scanning cancelled";
                }
                else
                {
                    if (blinkidRecognizer.Result.ResultState == RecognizerResultState.Valid)
                    {
                        var blinkidResult = blinkidRecognizer.Result;
                        stringResult =
                            "BlinkID recognizer result:\n" +
                            BuildResult(blinkidResult.FirstName, "First name") +
                            BuildResult(blinkidResult.LastName, "Last name") +
                            BuildResult(blinkidResult.FullName, "Full name") +
                            BuildResult(blinkidResult.LocalizedName, "Localized name") +
                            BuildResult(blinkidResult.AdditionalNameInformation, "Additional name info") +
                            BuildResult(blinkidResult.Address, "Address") +
                            BuildResult(blinkidResult.AdditionalAddressInformation, "Additional address info") +
                            BuildResult(blinkidResult.DocumentNumber, "Document number") +
                            BuildResult(blinkidResult.DocumentAdditionalNumber, "Additional document number") +
                            BuildResult(blinkidResult.Sex, "Sex") +
                            BuildResult(blinkidResult.IssuingAuthority, "Issuing authority") +
                            BuildResult(blinkidResult.Nationality, "Nationality") +
                            BuildResult(blinkidResult.DateOfBirth, "DateOfBirth") +
                            BuildResult(blinkidResult.Age, "Age") +
                            BuildResult(blinkidResult.DateOfIssue, "Date of issue") +
                            BuildResult(blinkidResult.DateOfExpiry, "Date of expiry") +
                            BuildResult(blinkidResult.DateOfExpiryPermanent, "Date of expiry permanent") +
                            BuildResult(blinkidResult.MaritalStatus, "Martial status") +
                            BuildResult(blinkidResult.PersonalIdNumber, "Personal Id Number") +
                            BuildResult(blinkidResult.Profession, "Profession") +
                            BuildResult(blinkidResult.Race, "Race") +
                            BuildResult(blinkidResult.Religion, "Religion") +
                            BuildResult(blinkidResult.ResidentialStatus, "Residential Status");

                        IDriverLicenseDetailedInfo licenceInfo = blinkidResult.DriverLicenseDetailedInfo;
                        if (licenceInfo != null)
                        {
                            stringResult +=
                                BuildResult(licenceInfo.Restrictions, "Restrictions") +
                                BuildResult(licenceInfo.Endorsements, "Endorsements") +
                                BuildResult(licenceInfo.VehicleClass, "Vehicle class") +
                                BuildResult(licenceInfo.Conditions, "Conditions");

                        }

                        fullDocumentFrontImageSource = blinkidResult.FullDocumentFrontImage;
                        fullDocumentBackImageSource = blinkidResult.FullDocumentBackImage;
                    }
                }

                Device.BeginInvokeOnMainThread(() => {
                    ResultsEditor = stringResult;
                    FullDocumentFrontImage = fullDocumentFrontImageSource;
                    FullDocumentBackImage = fullDocumentBackImageSource;
                    SuccessScanImage = successFrameImageSource;
                    FaceImage = faceImageSource;
                });

            });

            StartScanCommand = new Command(() =>
            {
                blinkidRecognizer = DependencyService.Get<IBlinkIdCombinedRecognizer>(DependencyFetchTarget.NewInstance);
                blinkidRecognizer.ReturnFullDocumentImage = true;
                var recognizerCollection = DependencyService.Get<IRecognizerCollectionFactory>().CreateRecognizerCollection(blinkidRecognizer);
                var blinkidOverlaySettings = DependencyService.Get<IBlinkIdOverlaySettingsFactory>().CreateBlinkIdOverlaySettings(recognizerCollection);
                blinkID.Scan(blinkidOverlaySettings);
            });
        }
        #endregion

        private string BuildResult(string result, string propertyName)
        {
            if (result == null || result.Length == 0)
            {
                return "";
            }

            return propertyName + ": " + result + "\n";
        }

        private string BuildResult(Boolean result, string propertyName)
        {
            if (result)
            {
                return propertyName + ": YES" + "\n";
            }

            return propertyName + ": NO" + "\n";
        }

        private string BuildResult(int result, string propertyName)
        {
            if (result < 0)
            {
                return "";
            }

            return propertyName + ": " + result + "\n";
        }

        private string BuildResult(IDate result, string propertyName)
        {
            if (result == null || result.Year == 0)
            {
                return "";
            }

            DateTime date = new DateTime(result.Year, result.Month, result.Day);
            return propertyName + ": " + date.ToShortDateString() + "\n";
        }


        #region Properties

        string resultsEditor;
        public string ResultsEditor
        {
            get => resultsEditor;
            set
            {
                SetProperty(ref resultsEditor, value);
            }
        }

        ImageSource fullDocumentFrontImage;
        public ImageSource FullDocumentFrontImage
        {
            get => fullDocumentFrontImage;
            set
            {
                SetProperty(ref fullDocumentFrontImage, value);
            }
        }

        ImageSource fullDocumentBackImage;
        public ImageSource FullDocumentBackImage
        {
            get => fullDocumentBackImage;
            set
            {
                SetProperty(ref fullDocumentBackImage, value);
            }
        }

        ImageSource successScanImage;
        public ImageSource SuccessScanImage
        {
            get => successScanImage;
            set
            {
                SetProperty(ref successScanImage, value);
            }
        }

        ImageSource faceImage;
        public ImageSource FaceImage
        {
            get => faceImage;
            set
            {
                SetProperty(ref faceImage, value);
            }
        }
        #endregion
        
        public Command StartScanCommand { get; }
    }
}
