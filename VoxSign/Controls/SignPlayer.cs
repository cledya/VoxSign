namespace Voxsign.Controls;
public class SignPlayer : Grid
{
    readonly Image a = new() { Aspect = Aspect.AspectFill };
    readonly Image b = new() { Aspect = Aspect.AspectFill, Opacity = 0 };
    bool front = true;
    public SignPlayer() { HeightRequest = 290; Children.Add(a); Children.Add(b); }

    public async Task ShowAsync(string src)
    {
        var (on, off) = front ? (b, a) : (a, b);
        on.Source = src; on.Scale = 1.1;
        await Task.WhenAll(on.FadeTo(1, 500), off.FadeTo(0, 500), on.ScaleTo(1, 500, Easing.CubicOut));
        front = !front;
    }
}
