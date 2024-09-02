class GroupPhase : IGroupPhase
{
    IGroupSimulator simulator;

    public GroupPhase(IGroupSimulator simulator)
    {
        this.simulator = simulator;
    }


    public List<Team> Start(List<Group> groups)
    {
        this.PlayGroups(groups);
        this.simulator.SortStandings(groups);
        this.PrintOut(groups);
        return this.simulator.FormHatRanking(groups);
    }

    void PlayGroups(List<Group> groups)
    {
        foreach (var group in groups)
        {
            PlayGroup(group);
        }
    }

    void PlayGroup(Group group)
    {
        for (int i = 0; i < group.Teams.Count; i++)
        {
            Team current = group.Teams[i];
            for (int j = i + 1; j < group.Teams.Count; j++)
            {
                simulator.PlayMatch(group, current, group.Teams[j]);
            }
        }
    }
    void PrintOut(List<Group> groups)
    {
        Console.WriteLine("\nGrupna faza utakmice:");
        foreach (var group in groups)
        {
            group.PrintMatches();
        }
        Console.WriteLine("\nKonačan plasman po grupama:");
        foreach (var group in groups)
        {
            group.PrintStandings();
        }
    }
}