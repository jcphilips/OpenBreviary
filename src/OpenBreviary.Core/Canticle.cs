using System.Diagnostics.CodeAnalysis;

namespace OpenBreviary.Core
{
  public class Canticle : LiturgicalElement
  {
    [SetsRequiredMembers]
    public Canticle(string book, int chapter, int[] verses, string content)
    {
      Book = book;
      Chapter = chapter;
      Verses = verses;
      Content = content;
    }
  }
}

