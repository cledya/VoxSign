using Voxsign.Services;
namespace Voxsign.Views;
public partial class TexteSignesPage : ContentPage
{
    public TexteSignesPage() { InitializeComponent(); }
    string FavId => "t:" + Msg.Text?.Trim();

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (!string.IsNullOrEmpty(SequenceState.Text)) Msg.Text = SequenceState.Text;
        FavLbl.Text = Favorites.Has(FavId) ? "♥  Retirer des favoris" : "♡  Ajouter cette phrase aux favoris";
    }
    async void OnIllustrer(object? s, EventArgs e)
    {
        SequenceState.Text = Msg.Text ?? "";
        SequenceState.Signs = AppData.Match(SequenceState.Text);
        await Shell.Current.GoToAsync("sequence");
    }
    async void OnSpeak(object? s, EventArgs e) => await Voice.SpeakAsync(Msg.Text ?? "");
    void OnClear(object? s, TappedEventArgs e) { Msg.Text = ""; SequenceState.Text = ""; }
    void OnFav(object? s, TappedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Msg.Text)) return;
        bool on = Favorites.Toggle(FavId);
        FavLbl.Text = on ? "♥  Retirer des favoris" : "♡  Ajouter cette phrase aux favoris";
    }
}
