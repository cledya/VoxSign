using Voxsign.Models;
using Voxsign.Services;
namespace Voxsign.Views;
public partial class SequencePage : ContentPage
{
    CancellationTokenSource? cts; int i;
    List<Sign> L => SequenceState.Signs;
    public SequencePage() { InitializeComponent(); }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        PhraseLbl.Text = SequenceState.Text;
        Thumbs.Children.Clear();
        for (int k = 0; k < L.Count; k++)
        {
            int idx = k;
            var t = new Border { Style = Ui.S("Card"), Padding = 6, WidthRequest = 110, Content = new VerticalStackLayout { Children = {
                new Image { Source = L[k].Image, HeightRequest = 70, Aspect = Aspect.AspectFill },
                new Label { Text = $"{k + 1} · {L[k].Word}", FontSize = 12, HorizontalOptions = LayoutOptions.Center } } } };
            t.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(async () => await Go(idx)) });
            Thumbs.Children.Add(t);
        }
        await Go(0);
    }
    protected override void OnDisappearing() { base.OnDisappearing(); cts?.Cancel(); }

    async Task Go(int k)
    {
        if (L.Count == 0) { WordLbl.Text = "Aucun repère"; Counter.Text = "0 sur 0"; return; }
        i = Math.Clamp(k, 0, L.Count - 1);
        var s = L[i]; SequenceState.Selected = s;
        WordLbl.Text = s.Word; Counter.Text = $"Repère {i + 1} sur {L.Count}";
        FavLbl.Text = Favorites.Has(s.Id) ? "♥" : "♡";
        await Player.ShowAsync(s.Image);
    }
    async void OnPrev(object? s, EventArgs e) => await Go(i - 1);
    async void OnNext(object? s, EventArgs e) => await Go(i + 1);
    async void OnPlay(object? s, EventArgs e)
    {
        cts?.Cancel(); cts = new();
        try { for (int k = 0; k < L.Count; k++) { await Go(k); await Task.Delay(900, cts.Token); } }
        catch (OperationCanceledException) { }
    }
    void OnFav(object? s, TappedEventArgs e)
    { if (L.Count > 0) FavLbl.Text = Favorites.Toggle(L[i].Id) ? "♥" : "♡"; }
    async void OnSpeak(object? s, EventArgs e) => await Voice.SpeakAsync(SequenceState.Text);
    async void OnShare(object? s, EventArgs e) =>
        await Share.Default.RequestAsync(new ShareTextRequest { Text = $"{SequenceState.Text}\nRepères : {string.Join(" → ", L.Select(x => x.Word))}", Title = "VoxSign" });
}
