class SetupSimulator : ISetupSimulator
{
    public void ResetStandings(List<Group> groups)
    {
        foreach (var group in groups)
        {
            int i = 1;
            foreach (var team in group.Teams)
            {
                group.Standings.Add(new Standing(team.Rank, team.Name, 0, 0, 0, 0, 0, 0));
                i++;
            }
        }
    }
}