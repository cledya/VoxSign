using Voxsign.Services;
namespace Voxsign.Views;
public partial class FavorisPage : ContentPage
{
    public FavorisPage() { InitializeComponent(); }
    protected override void OnAppearing() { base.OnAppearing(); Refresh(); }
    void Refresh()
    {
        SignsBox.Children.Clear(); PhrasesBox.Children.Clear();
        int ns = 0, np = 0;
        foreach (var id in Favorites.All.ToList())
        {
            var sign = AppData.Signs.FirstOrDefault(s => s.Id == id);
            if (sign != null) { SignsBox.Children.Add(Ui.SignCard(sign)); ns++; continue; }
            var text = id.StartsWith("t:") ? id[2..] : AppData.Phrases.FirstOrDefault(p => p.Id == id)?.Text;
            if (text != null) { PhrasesBox.Children.Add(Ui.PhraseRow(id, text, "Enregistrée", Refresh)); np++; }
        }
        Total.Text = $"Tout · {ns + np}  |  Signes · {ns}  |  Phrases · {np}";
    }
}
