public class Group
{
    string name;
    List<Team> teams;
    List<Standing> standings;
    List<MatchDisplay> matches;

    public Group(string name, IEnumerable<Team> teams = null, IEnumerable<Standing> standings = null, IEnumerable<MatchDisplay> matches = null)
    {
        this.name = name;
        this.teams = teams?.ToList() ?? new List<Team>();
        this.standings = standings?.ToList() ?? new List<Standing>();
        this.Matches = matches?.ToList() ?? new List<MatchDisplay>();
    }

    public void PrintMatches()
    {
        Console.WriteLine("\tGrupa " + Name + ":");
        foreach (var match in Matches)
            Console.WriteLine("\t\t" + match.Home + " - " + match.Visitor + " (" + match.Result.Replace('-', ':') + ")");
    }

    public void PrintStandings()
    {
        int max = 0;
        foreach (var team in teams)
        {
            if (team.Name.Length > max)
            {
                max = team.Name.Length;
            }
        }
        Console.WriteLine("\tGrupa " + Name + " (Ime - pobede/porazi/bodovi/postignuti koševi/primljeni koševi/koš razlika):");
        int i = 1;
        foreach (var standing in Standings)
            Console.WriteLine("\t\t" + i++ + ". " + standing.Name.PadRight(max - Name.Length) + "\t" + standing.Wins + " / " + standing.Losses + " / " + standing.Points + " / " + standing.Scored + " / " + standing.Received + " / " + standing.ScoreDiff);
    }
    public string Name { get => name; set => name = value; }
    public List<Team> Teams { get => teams; set => teams = value; }
    public List<Standing> Standings { get => standings; set => standings = value; }
    public List<MatchDisplay> Matches { get => matches; set => matches = value; }
}