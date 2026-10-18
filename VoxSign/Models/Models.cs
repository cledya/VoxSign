namespace Voxsign.Models;
public record Sign(string Id, string Word, string Category, string Image, string Hint, string Example);
public record Phrase(string Id, string Text, string Context, string Category);
