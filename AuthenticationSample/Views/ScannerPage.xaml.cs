using BarcodeScanning;

namespace AuthenticationSample.Views;

public partial class ScannerPage : ContentPage
{
	public ScannerPage()
	{
		InitializeComponent();

        Unloaded += (sender, e) => { BarcodeScanner.Handler.DisconnectHandler(); };
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        BarcodeScanner.CameraEnabled = true;

        await Methods.AskForRequiredPermissionAsync();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        BarcodeScanner.CameraEnabled = false;

    }

}