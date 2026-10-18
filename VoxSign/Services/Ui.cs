using Voxsign.Models;
namespace Voxsign.Services;
public static class Ui
{
    public static Style S(string k) => (Style)Application.Current!.Resources[k];

    public static Border SignCard(Sign s, double w = 165)
    {
        var b = new Border { Style = S("Card"), Padding = 10, WidthRequest = w, Margin = 4,
            Content = new VerticalStackLayout { Spacing = 6, Children = {
                new Border { Style = S("Photo"), HeightRequest = 130, Content = new Image { Source = s.Image, Aspect = Aspect.AspectFill } },
                new Label { Text = s.Word, FontSize = 18, FontAttributes = FontAttributes.Bold },
                new Label { Text = s.Category, Style = S("Muted") } } } };
        b.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(async () =>
            { SequenceState.Selected = s; await Shell.Current.GoToAsync("signe"); }) });
        return b;
    }

    public static Border PhraseRow(string id, string text, string sub, Action? refresh = null)
    {
        var fav = new Label { Text = Favorites.Has(id) ? "♥" : "♡", TextColor = (Color)Application.Current!.Resources["Primary"], FontSize = 24, VerticalOptions = LayoutOptions.Center };
        fav.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(() =>
            { Favorites.Toggle(id); fav.Text = Favorites.Has(id) ? "♥" : "♡"; refresh?.Invoke(); }) });
        var g = new Grid { ColumnDefinitions = { new ColumnDefinition(GridLength.Star), new ColumnDefinition(GridLength.Auto) } };
        g.Add(new VerticalStackLayout { Spacing = 6, Children = { new Label { Text = text, FontSize = 18 }, new Label { Text = sub, Style = S("Muted") } } });
        g.Add(fav, 1);
        var b = new Border { Style = S("Card"), Content = g };
        b.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(async () =>
            { SequenceState.Text = text; await Shell.Current.GoToAsync("texte"); }) });
        return b;
    }
}
