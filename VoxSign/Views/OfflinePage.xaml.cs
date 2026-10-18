namespace Voxsign.Views;

public partial class OfflinePage : ContentPage
{
    public OfflinePage() => InitializeComponent();

    async void OnDownload(object? s, TappedEventArgs e)
    {
        if (Connectivity.Current.ConnectionProfiles.Contains(ConnectionProfile.WiFi))
            await DisplayAlert("Téléchargement", "Le pack « Santé & besoins » (18 Mo) va être téléchargé.", "OK");
        else
            await DisplayAlert("Wi-Fi requis", "Connectez-vous en Wi-Fi pour télécharger ce pack.", "OK");
    }
}
