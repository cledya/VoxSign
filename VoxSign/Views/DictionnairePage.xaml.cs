using Voxsign.Services;
namespace Voxsign.Views;
public partial class DictionnairePage : ContentPage
{
    string cat = "Tous";
    public DictionnairePage() { InitializeComponent(); BuildChips(); }
    protected override void OnAppearing() { base.OnAppearing(); Refresh(); }

    void BuildChips()
    {
        Chips.Children.Clear();
        foreach (var c in new[] { "Tous" }.Concat(AppData.Signs.Select(s => s.Category).Distinct()))
        {
            var b = new Button { Text = c, Style = Ui.S(c == cat ? "ChipOn" : "Chip"), Padding = new Thickness(16, 0) };
            b.Clicked += (_, _) => { cat = c; BuildChips(); Refresh(); };
            Chips.Children.Add(b);
        }
    }
    void Refresh()
    {
        Cards.Children.Clear();
        var q = AppData.Norm(Search.Text ?? "");
        var list = AppData.Signs.Where(s => (cat == "Tous" || s.Category == cat) && AppData.Norm(s.Word).Contains(q)).ToList();
        foreach (var s in list) Cards.Children.Add(Ui.SignCard(s));
        Count.Text = $"{list.Count} repères";
    }
    void OnSearch(object? s, TextChangedEventArgs e) => Refresh();
}
