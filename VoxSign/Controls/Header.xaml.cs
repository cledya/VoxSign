namespace Voxsign.Controls;
public partial class Header : ContentView
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(Header), "");
    public static readonly BindableProperty SubtitleProperty = BindableProperty.Create(nameof(Subtitle), typeof(string), typeof(Header), "");
    public static readonly BindableProperty RightProperty = BindableProperty.Create(nameof(Right), typeof(string), typeof(Header), "Options");
    public string Title { get => (string)GetValue(TitleProperty); set => SetValue(TitleProperty, value); }
    public string Subtitle { get => (string)GetValue(SubtitleProperty); set => SetValue(SubtitleProperty, value); }
    public string Right { get => (string)GetValue(RightProperty); set => SetValue(RightProperty, value); }
    public Header() { InitializeComponent(); }
}
