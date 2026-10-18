using Microsoft.Maui.Controls.Shapes;
using Voxsign.Helpers;
using Voxsign.Models;

namespace Voxsign.Views;

public partial class FavoritesPage : ContentPage
{
    readonly List<(Border Box, Label Text, string Key)> _chips = new();
    string _filter = "all";

    public FavoritesPage()
    {
        InitializeComponent();
        AddChip("all", 0); AddChip("signs", 1); AddChip("phrases", 2);
    }

    void AddChip(string key, int col)
    {
        var label = new Label { FontFamily = "InterBold", FontSize = 17, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center };
        var box = new Border { HeightRequest = 56, StrokeThickness = 1, StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(18) }, Content = label };
        var tap = new TapGestureRecognizer();
        tap.Tapped += (_, _) => { _filter = key; Refresh(); };
        box.GestureRecognizers.Add(tap);
        _chips.Add((box, label, key));
        FilterGrid.Add(box, col, 0);
    }

    protected override void OnAppearing() { base.OnAppearing(); Refresh(); }

    void Refresh()
    {
        var signs = SampleData.Signs.Where(s => s.IsFavorite).ToList();
        var phrases = SampleData.Phrases.Where(p => p.IsFavorite).ToList();

        foreach (var (box, text, key) in _chips)
        {
            text.Text = key switch
            {
                "all" => $"Tout · {signs.Count + phrases.Count}",
                "signs" => $"Signes · {signs.Count}",
                _ => $"Phrases · {phrases.Count}"
            };
            bool on = key == _filter;
            box.BackgroundColor = on ? Theme.Primary : Colors.White;
            box.Stroke = on ? Theme.Primary : Theme.Line;
            text.TextColor = on ? Colors.White : Theme.Muted;
        }

        SignsSection.IsVisible = _filter != "phrases";
        PhrasesSection.IsVisible = _filter != "signs";
        SignsCount.Text = $"{signs.Count} enregistré{(signs.Count > 1 ? "s" : "")}";
        PhrasesCount.Text = $"{phrases.Count} phrase{(phrases.Count > 1 ? "s" : "")}";
        BindableLayout.SetItemsSource(SignsLayout, signs);
        BindableLayout.SetItemsSource(PhraseList, phrases);
    }

    void OnSignBookmark(object? s, TappedEventArgs e)
    {
        if (s is BindableObject { BindingContext: SignItem x }) { x.IsFavorite = false; Refresh(); }
    }

    void OnPhraseBookmark(object? s, TappedEventArgs e)
    {
        if (s is BindableObject { BindingContext: PhraseItem p }) { p.IsFavorite = false; Refresh(); }
    }

    async void OnSignTapped(object? s, TappedEventArgs e)
    {
        if (s is BindableObject { BindingContext: SignItem x })
            await Shell.Current.GoToAsync("signdetail", new Dictionary<string, object> { ["Sign"] = x });
    }
}
