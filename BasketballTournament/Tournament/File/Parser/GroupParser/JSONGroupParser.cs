class JSONGroupParser : JSONParser, IJSONGroupParser
{
    //JSONExhibitionsParser jep;
    public JSONGroupParser(IFileHandler handler/*, JSONExhibitionsParser jep*/) : base(handler)
    {
        //this.jep = jep;
    }

    public List<Team> ParseGroup(string name)
    {
        List<Team> teams = new List<Team>();
        foreach (var team in Root.GetProperty(name).EnumerateArray())
        {
            // string team = e.GetProperty("Team").GetString();
            // string iso = e.GetProperty("ISOCode").GetString();
            // int rank = e.GetProperty("FIBARanking").GetInt32();
            //teams.Add(new Team(team, iso, rank, jep.ParseTeamResults(iso)));
            teams.Add(new Team(team.GetProperty("Team").GetString(), team.GetProperty("ISOCode").GetString(), team.GetProperty("FIBARanking").GetInt32()));
        }

        return teams;
    }

    public List<Group> ExtractGroups()
    {
        List<Group> groups = new List<Group>();

        foreach (var group in Root.EnumerateObject().ToList())
        {
            groups.Add(new Group(group.Name, ParseGroup(group.Name)));
        }

        return groups;
    }
}