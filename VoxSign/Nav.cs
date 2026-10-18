using System.Windows.Input;
namespace Voxsign;
public class Nav
{
    public static Nav I { get; } = new();
    public ICommand Go { get; } = new Command<string>(async r => await Shell.Current.GoToAsync(r));
}
