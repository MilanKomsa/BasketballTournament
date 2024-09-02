using System.ComponentModel.DataAnnotations;

public class Team
{
    string name;
    string isoCode;
    int rank;
    List<Match> matches;
    public Team(string name, string isoCode, int rank, List<Match> matches = null)
    {
        this.Name = name;
        this.IsoCode = isoCode;
        this.Rank = rank;
        this.matches = matches?.ToList() ?? new List<Match>();
    }

    public string Name { get => name; set => name = value; }
    public string IsoCode { get => isoCode; set => isoCode = value; }
    public int Rank { get => rank; set => rank = value; }
    public List<Match> Matches { get => matches; set => matches = value; }
}