namespace Voxsign.Services;
public static class Speech
{
    static CancellationTokenSource? cts;
    public static async Task SpeakAsync(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return;
        cts?.Cancel(); cts = new();
        try
        {
            var fr = (await TextToSpeech.Default.GetLocalesAsync()).FirstOrDefault(l => l.Language == "fr");
            await TextToSpeech.Default.SpeakAsync(text, new SpeechOptions { Locale = fr }, cts.Token);
        }
        catch (OperationCanceledException) { }
    }
}
