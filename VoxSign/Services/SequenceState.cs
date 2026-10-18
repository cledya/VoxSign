using Voxsign.Models;
namespace Voxsign.Services;
// État partagé simple entre les pages
public static class SequenceState
{
    public static string Text = "";
    public static List<Sign> Signs = new();
    public static Sign? Selected;
}
