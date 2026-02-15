using System.Diagnostics.CodeAnalysis;

namespace OpenBreviary.Core
{
  public class Psalm : LiturgicalElement
  {
    public int VulgateNumber
    { get; set; }

    public int HebrewNumber
    { get; set; }

    public string GetLabel => $"Psalm {VulgateNumber} ({HebrewNumber})";

    public string Antiphon
    { get; set; }

    public bool DoGloriaBeforeAntiphon
    { get; set; } = true;

    [SetsRequiredMembers]
    public Psalm(int vulgate, int hebrew, int[] verses, Dictionary<int, string> content, string antiphon)
    {
      Book = "Psalms";
      Chapter = vulgate;
      VulgateNumber = vulgate;
      HebrewNumber = hebrew;
      Verses = verses;
      Content = content;
      Antiphon = antiphon;
    }
  }
}

