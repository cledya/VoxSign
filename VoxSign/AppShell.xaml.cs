using Voxsign.Views;

namespace Voxsign;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("register", typeof(RegisterPage));
        Routing.RegisterRoute("texte", typeof(TexteSignesPage));
        Routing.RegisterRoute("voix", typeof(VoixTextePage));
        Routing.RegisterRoute("sequence", typeof(SequencePage));
        Routing.RegisterRoute("signe", typeof(SigneDetailPage));
        Routing.RegisterRoute("phrases", typeof(PhrasesPage));
        Routing.RegisterRoute("offline", typeof(HorsConnexionPage));
    }
}