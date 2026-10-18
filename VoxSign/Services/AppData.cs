using System.Globalization;
using System.Text;
using Voxsign.Models;
namespace Voxsign.Services;

// Données embarquées dans l'app => fonctionnent 100 % hors ligne.
public static class AppData
{
    public static readonly List<Sign> Signs = new()
    {
        new("bonjour","Bonjour","Politesse","bonjour.jpg","Salutation d’accueil","Bonjour, comment allez-vous ?"),
        new("merci","Merci","Politesse","merci.jpg","Exprimer sa gratitude après un service","Merci pour votre aide."),
        new("cafe","Café","Au quotidien","cafe.jpg","Commander une boisson","Un café, s’il vous plaît."),
        new("aide","Aide","Santé & besoins","aide.jpg","Demander de l’aide","J’ai besoin d’aide."),
        new("svp","S’il vous plaît","Politesse","sil_vous_plait.jpg","Formule de politesse","À emporter, s’il vous plaît."),
    };
    public static readonly List<Phrase> Phrases = new()
    {
        new("p1","Bonjour, je voudrais un café, s’il vous plaît.","Commander","Au café"),
        new("p2","À emporter, s’il vous plaît.","Préciser","Au café"),
        new("p3","Combien ça coûte ?","Payer","Au café"),
        new("p4","Merci, bonne journée !","Saluer","Au café"),
        new("p5","Je suis sourde. Écrivez-moi, s’il vous plaît.","Me présenter","Santé"),
        new("p6","Pouvez-vous répéter plus lentement ?","Se comprendre","Santé"),
    };

    public static string Norm(string s)
    {
        var sb = new StringBuilder();
        foreach (var c in s.ToLowerInvariant().Normalize(NormalizationForm.FormD))
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                sb.Append(c == '’' ? '\'' : c);
        return sb.ToString();
    }

    // Texte -> liste ordonnée de repères illustrés
    public static List<Sign> Match(string text)
    {
        var n = Norm(text);
        return Signs.Select(s => (i: n.IndexOf(Norm(s.Word), StringComparison.Ordinal), s))
                    .Where(x => x.i >= 0).OrderBy(x => x.i).Select(x => x.s).ToList();
    }
}
