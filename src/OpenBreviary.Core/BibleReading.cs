using System.Diagnostics.CodeAnalysis;

namespace OpenBreviary.Core
{
  public class BibleReading : LiturgicalElement
  {
    [SetsRequiredMembers]
    public BibleReading(string book, int chapter, int[] verses, string content)
    {
      Book = book;
      Chapter = chapter;
      Verses = verses;
      Content = content;
    }
  }
}


