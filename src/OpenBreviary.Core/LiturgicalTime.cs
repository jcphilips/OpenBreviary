namespace OpenBreviary.Core
{
  public class LiturgicalTime
  {
    public DateTime Date { get; set; }
    public LiturgicalSeason Season { get; set; }
    public int WeekOfSeason { get; set; }
    public DayOfWeek DayOfWeek => Date.DayOfWeek;
    public int PsalterWeek { get; set; }
    public string Description { get; set; } = string.Empty;
  }
}
