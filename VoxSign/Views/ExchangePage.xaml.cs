using Microsoft.Maui.Controls.Shapes;
using Voxsign.Helpers;

namespace Voxsign.Views;

public partial class ExchangePage : ContentPage
{
    public ExchangePage() => InitializeComponent();

    void OnSend(object? s, EventArgs e) => Send();
    void OnSendTapped(object? s, TappedEventArgs e) => Send();

    async void Send()
    {
        var text = MessageEntry.Text?.Trim();
        if (string.IsNullOrEmpty(text)) return;

        Messages.Children.Add(new Border
        {
            BackgroundColor = Colors.White, Stroke = Theme.Line, StrokeThickness = 1, Padding = new Thickness(20, 16),
            StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(22) },
            HorizontalOptions = LayoutOptions.End, MaximumWidthRequest = 300,
            Content = new VerticalStackLayout
            {
                Spacing = 8,
                Children =
                {
                    new Label { Text = text, FontSize = 20 },
                    new Label { Text = $"Vous · {DateTime.Now:H:mm}", TextColor = Theme.Primary, FontSize = 14 }
                }
            }
        });
        MessageEntry.Text = string.Empty;
        await Task.Delay(50);
        await Scroller.ScrollToAsync(0, Scroller.ContentSize.Height, true);
    }

    async void OnSharePhrase(object? s, EventArgs e) => await Shell.Current.GoToAsync("phrases");
}
