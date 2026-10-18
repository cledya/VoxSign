namespace Voxsign.Services;

public static class Voice
{
    static CancellationTokenSource? cts;

    public static async Task SpeakAsync(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return;
        cts?.Cancel();
        cts = new();
        try
        {
            var locales = await Microsoft.Maui.Media.TextToSpeech.Default.GetLocalesAsync();
            var fr = locales.FirstOrDefault(l => l.Language == "fr");
            await Microsoft.Maui.Media.TextToSpeech.Default.SpeakAsync(
                text, new Microsoft.Maui.Media.SpeechOptions { Locale = fr }, cts.Token);
        }
        catch (OperationCanceledException) { }
    }
}