using Voxsign.Services;
namespace Voxsign.Views;
public partial class PhrasesPage : ContentPage
{
    string cat = "Au café";
    public PhrasesPage() { InitializeComponent(); }
    protected override void OnAppearing() { base.OnAppearing(); Build(); }
    void Build()
    {
        Chips.Children.Clear();
        foreach (var c in AppData.Phrases.Select(p => p.Category).Distinct())
        {
            var b = new Button { Text = c, Style = Ui.S(c == cat ? "ChipOn" : "Chip"), Padding = new Thickness(18, 0) };
            b.Clicked += (_, _) => { cat = c; Build(); };
            Chips.Children.Add(b);
        }
        List.Children.Clear();
        foreach (var p in AppData.Phrases.Where(p => p.Category == cat))
            List.Children.Add(Ui.PhraseRow(p.Id, p.Text, $"{p.Context} · Illustrer ou lire"));
    }
}
