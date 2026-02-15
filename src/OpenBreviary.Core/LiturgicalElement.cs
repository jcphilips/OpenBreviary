namespace OpenBreviary.Core
{
  public abstract class LiturgicalElement
  {
    public string Language
    { get; set; } = "English";

    public required string Book
    { get; set; } = string.Empty;

    public required int Chapter
    { get; set; }

    public required int[] Verses
    { get; set; } = Array.Empty<int>();

    public required Dictionary<int, string> Content
    { get; set; } = new();
  }
}
