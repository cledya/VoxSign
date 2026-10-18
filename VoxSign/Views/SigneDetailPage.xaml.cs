using Voxsign.Services;
namespace Voxsign.Views;
public partial class SigneDetailPage : ContentPage
{
    public SigneDetailPage() { InitializeComponent(); }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        var s = SequenceState.Selected ?? AppData.Signs[0];
        Img.Source = s.Image; WordLbl.Text = s.Word; CatLbl.Text = $"Dictionnaire · {s.Category}";
        HintLbl.Text = s.Hint; ExLbl.Text = $"« {s.Example} »"; SpeakBtn.Text = $"🔊  Lire le mot « {s.Word} »";
        FavLbl.Text = Favorites.Has(s.Id) ? "♥" : "♡";
    }
    void OnFav(object? s, TappedEventArgs e) => FavLbl.Text = Favorites.Toggle(SequenceState.Selected!.Id) ? "♥" : "♡";
    async void OnSpeak(object? s, EventArgs e) => await Voice.SpeakAsync(SequenceState.Selected!.Word);
}
