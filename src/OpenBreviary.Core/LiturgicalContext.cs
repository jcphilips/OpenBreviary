namespace OpenBreviary.Core
{
  public record LiturgicalContext(
      DateTime Date,
      LiturgicalSeason Season,
      MoveableFeasts Feasts,
      int LiturgicalYear,
      LiturgicalConfiguration Configuration = null
      );
}
