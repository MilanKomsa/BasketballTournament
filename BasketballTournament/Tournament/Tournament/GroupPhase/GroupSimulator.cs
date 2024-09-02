public class GroupSimulator : IGroupSimulator
{
    IHelper helper;
    public GroupSimulator(IHelper helper)
    {
        this.helper = helper;
    }

    public void PlayMatch(Group g, Team t1, Team t2)
    {
        Random rand = new Random();
        DateTime date = DecideDate(t1, t2).AddDays(rand.Next(1, 8));
        Standing s1 = g.Standings.Where(s => s.Name == t1.Name).First();
        Standing s2 = g.Standings.Where(s => s.Name == t2.Name).First();
        if (rand.NextSingle() > 0.01)
        {
            int scoreL = rand.Next(21, 101);
            int scoreW = scoreL + rand.Next(1, 41);
            double prob = helper.CalculateProbabilities(t1, t2);//probability t1 wins
            if (rand.NextSingle() < prob)
            {
                s1.UpdateStandingWinner(scoreW, scoreL);
                s2.UpdateStandingLosser(scoreL, scoreW);
                t1.Matches.Add(new Match(date, t2.IsoCode, scoreW + "-" + scoreL));
                t2.Matches.Add(new Match(date, t1.IsoCode, scoreL + "-" + scoreW));
            }
            else
            {
                s2.UpdateStandingWinner(scoreW, scoreL);
                s1.UpdateStandingLosser(scoreL, scoreW);
                t1.Matches.Add(new Match(date, t2.IsoCode, scoreL + "-" + scoreW));
                t2.Matches.Add(new Match(date, t1.IsoCode, scoreW + "-" + scoreL));
            }
        }
        else
        {
            if (rand.NextSingle() <= 0.5)
            {
                s1.UpdateStandingWinnerBySurrender();
                s2.UpdateStandingLosserBySurrender();
                t1.Matches.Add(new Match(date, t2.IsoCode, 20 + "-" + 0));
                t2.Matches.Add(new Match(date, t1.IsoCode, 0 + "-" + 20));
            }
            else
            {
                s2.UpdateStandingWinnerBySurrender();
                s1.UpdateStandingLosserBySurrender();
                t1.Matches.Add(new Match(date, t2.IsoCode, 0 + "-" + 20));
                t2.Matches.Add(new Match(date, t1.IsoCode, 20 + "-" + 0));
            }
        }
        g.Matches.Add(new MatchDisplay(t1.Name, t2.Name, t1.Matches.Last().Result));
    }

    DateTime DecideDate(Team t1, Team t2)
    {
        DateTime date1 = t1.Matches.Last().Date;
        DateTime date2 = t2.Matches.Last().Date;
        if (date1 > date2)
        {
            return date1;
        }
        else
        {
            return date2;
        }
    }

    public void SortStandings(List<Group> groups)
    {
        List<List<int>> indexes = new List<List<int>>();

        foreach (var group in groups)
        {
            group.Standings.Sort();

            for (int i = 0; i < group.Teams.Count - 2; i++)
            {
                if (group.Standings[i].Points == group.Standings[i + 1].Points && group.Standings[i].Points == group.Standings[i + 2].Points)
                {
                    indexes.Add(new List<int> { i, i + 1, i + 2 });
                }
            }
            if (!indexes.Any())
            {
                for (int i = 0; i < group.Teams.Count - 1; i++)
                {
                    if (group.Standings[i].Points == group.Standings[i + 1].Points)
                    {
                        indexes.Add(new List<int> { i, i + 1 });
                    }
                }
            }

            List<int> newIndexes = new List<int>();
            foreach (var index in indexes)
            {
                List<Standing> modifiedStandings = ModifyStandings(group, index);
                modifiedStandings.Sort();
                int current = 0;
                foreach (var i in index)
                {
                    if (group.Standings[i].Name != modifiedStandings[current].Name)
                    {
                        newIndexes.Add(group.Standings.FindIndex(s => s.Name == modifiedStandings[current].Name));
                    }
                    else
                    {
                        newIndexes.Add(i);
                    }
                    current++;
                }
                if (!newIndexes.SequenceEqual(index))
                {
                    Standing[] temp = new Standing[index.Count];
                    for (int i = 0; i < index.Count; i++)
                    {
                        temp[i] = group.Standings[index[i]];
                    }
                    for (int i = 0; i < index.Count; i++)
                    {
                        group.Standings[newIndexes[i]] = temp[i];
                    }

                }
                newIndexes.Clear();
            }
            indexes.Clear();
        }
    }

    //command/memento pattern for standings/playmatch would be great here
    List<Standing> ModifyStandings(Group group, List<int> indexes)
    {
        List<Standing> modifiedStandings = new List<Standing>();
        List<Standing> undoedStandings = new List<Standing>();

        List<Standing> outStandings = new List<Standing>();

        modifiedStandings = indexes.Select(index => group.Standings[index]).ToList();
        undoedStandings = group.Standings.Except(modifiedStandings).ToList();

        foreach (var standing in modifiedStandings)
        {
            int scoredDeduct = 0;
            int receivedDeduct = 0;
            int diffDeduct = 0;
            foreach (var undoed in undoedStandings)
            {
                string iso = group.Teams.Find(t => t.Name == undoed.Name).IsoCode;
                var match = group.Teams.Find(t => t.Name == standing.Name).Matches.FindLast(m => m.Opponent == iso);
                string result = match.Result;
                int[] points = Array.ConvertAll(result.Split('-'), int.Parse);
                scoredDeduct += points[0];
                receivedDeduct += points[1];
                diffDeduct += points[0] - points[1];
            }
            outStandings.Add(new Standing(standing, scoredDeduct, receivedDeduct, diffDeduct));
        }

        return outStandings;
    }

    public List<Team> FormHatRanking(List<Group> groups)
    {
        List<Team> hatDrawList = new List<Team>();
        List<Standing> sortedStandings = new List<Standing>();

        for (int i = 0; i < 3; i++)
        {
            foreach (var group in groups)
            {
                sortedStandings.Add(group.Standings[i]);
            }
            sortedStandings.Sort();

            foreach (var standing in sortedStandings)
            {
                hatDrawList.Add(groups.Find(g => g.Teams.Any(t => t.Name == standing.Name)).Teams.Find(t => t.Name == standing.Name));
            }
            sortedStandings.Clear();
        }

        return hatDrawList.Take(8).ToList();
    }
}
