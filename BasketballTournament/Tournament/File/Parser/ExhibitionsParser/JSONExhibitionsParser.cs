class JSONExhibitionsParser : JSONParser, IJSONExhibitionsParser
{
    public JSONExhibitionsParser(IFileHandler handler) : base(handler)
    {

    }
    public List<Match> ParseTeamResults(string ISOCode)
    {
        List<Match> matches = new List<Match>();

        foreach (var e in Root.GetProperty(ISOCode).EnumerateArray())
        {
            matches.Add(new Match(DateTime.ParseExact(e.GetProperty("Date").GetString(), "dd/MM/yy", null), e.GetProperty("Opponent").GetString(), e.GetProperty("Result").GetString()));
        }

        return matches;
    }
}