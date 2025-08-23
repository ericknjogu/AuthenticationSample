using AuthenticationSample.Services;
using BarcodeScanning;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationSample.ViewModels
{
    public partial class ScannerViewModel:ObservableObject
    {
        DatabaseService _db = new DatabaseService();
        [ObservableProperty]
        private string scannedResult;
        public ScannerViewModel()
        {
        }

        [RelayCommand]

        public void DetectionFinished(IReadOnlySet<BarcodeResult> results)
        {

            foreach (var barcode in results)
            {
                ScannedResult = barcode.RawValue;
                break;

            }
        }

        [RelayCommand]
        public async Task SaveQRcodes()
        {
            try
            {
                int r = await _db.SaveQrCode(ScannedResult, DateTime.Now);

                if (r > 0)
                {
                    ScannedResult = string.Empty;
                    await App.Current.MainPage.DisplayAlert("Success", "Qrcode saved Succesfully", "OK");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Failed", "Saving failed try again later", "Ok");
                }

            }
            catch (Exception ex)
            {

                await App.Current.MainPage.DisplayAlert("Failed", "An error occured"+ex, "Ok");
            }
           
        }
    }
}
