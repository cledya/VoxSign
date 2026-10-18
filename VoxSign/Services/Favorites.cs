namespace Voxsign.Services;
public static class Favorites
{
    const string Key = "favs";
    static readonly HashSet<string> set = new(Preferences.Get(Key, "").Split('|', StringSplitOptions.RemoveEmptyEntries));
    public static IEnumerable<string> All => set;
    public static bool Has(string id) => set.Contains(id);
    public static bool Toggle(string id)
    {
        bool on = set.Add(id);
        if (!on) set.Remove(id);
        Preferences.Set(Key, string.Join('|', set));   // persistant, fonctionne hors ligne
        return on;
    }
}
