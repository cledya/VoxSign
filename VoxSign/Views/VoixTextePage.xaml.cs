namespace Voxsign.Views;
public partial class VoixTextePage : ContentPage
{
    public VoixTextePage() { InitializeComponent(); }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            await DisplayAlertAsync("Hors connexion", "La reconnaissance vocale nécessite Internet. Vos packs et favoris restent disponibles.", "OK");
    }
}
