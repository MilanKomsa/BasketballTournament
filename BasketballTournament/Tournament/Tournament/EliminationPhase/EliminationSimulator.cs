
class EliminationSimulator : IEliminationSimulator
{
    IHelper helper;
    public EliminationSimulator(IHelper helper)
    {
        this.helper = helper;
    }
    public void PlayFinals(List<Pair> finalPairs)
    {
        List<Team> winners = new List<Team>(new Team[3]);
        Console.WriteLine("\nUtakmica za treće mesto:");
        winners[2] = this.PlayMatchQuarters(finalPairs[0].T1, finalPairs[0].T2);
        Console.WriteLine("\nFinale:");
        List<Team> finalsResult = this.PlayMatchSemis(finalPairs[1].T1, finalPairs[1].T2);
        winners[1] = finalsResult[0];
        winners[0] = finalsResult[1];

        Console.WriteLine("\nMedalje");
        int place = 1;
        foreach (var team in winners)
        {
            Console.WriteLine("\t" + place++ + ". " + team.Name);
        }
    }

    public List<Pair> PlaySemiFinals(List<Pair> semiPairs)
    {
        Console.WriteLine("\nPolufinale:");
        List<Pair> finalPairs = new List<Pair>();

        finalPairs.AddRange(this.AdvanceToFinals(semiPairs[0], semiPairs[1]));

        Console.WriteLine();

        return finalPairs;
    }
    List<Pair> AdvanceToFinals(Pair pair1, Pair pair2)
    {
        List<Pair> finalPairs = new List<Pair>();
        List<Team> teams1 = PlayMatchSemis(pair1.T1, pair1.T2);
        List<Team> teams2 = PlayMatchSemis(pair2.T1, pair2.T2);
        finalPairs.Add(new Pair(Stage.Final, teams1[0], teams2[0]));
        finalPairs.Add(new Pair(Stage.Final, teams1[1], teams2[1]));
        return finalPairs;
    }
    List<Team> PlayMatchSemis(Team t1, Team t2)
    {
        Random rand = new Random();
        int scoreL = rand.Next(21, 101);
        int scoreW = scoreL + rand.Next(1, 41);
        double prob = helper.CalculateProbabilities(t1, t2);
        if (rand.NextSingle() < prob)
        {
            Console.WriteLine("\t" + t1.Name + " - " + t2.Name + " (" + scoreW + ":" + scoreL + ")");
            return new List<Team> { t2, t1 };
        }
        else
        {
            Console.WriteLine("\t" + t1.Name + " - " + t2.Name + " (" + scoreL + ":" + scoreW + ")");
            return new List<Team> { t1, t2 };
        }
    }
    public List<Pair> PlayQuarterFinals(List<Pair> quarterPairs)
    {
        Console.WriteLine("\nČetvrtfinale:");
        List<Pair> semiPairs = new List<Pair>();
        for (int i = 0; i < 2; i++)
        {
            semiPairs.Add(this.AdvanceToSemis(quarterPairs[i * 2], quarterPairs[i * 2 + 1]));
            Console.WriteLine();
        }
        return semiPairs;
    }

    Pair AdvanceToSemis(Pair pair1, Pair pair2)
    {
        Team t1 = PlayMatchQuarters(pair1.T1, pair1.T2);
        Team t2 = PlayMatchQuarters(pair2.T1, pair2.T2);
        return new Pair(Stage.Semi, t1, t2);
    }
    Team PlayMatchQuarters(Team t1, Team t2)
    {
        Random rand = new Random();
        int scoreL = rand.Next(21, 101);
        int scoreW = scoreL + rand.Next(1, 41);
        double prob = helper.CalculateProbabilities(t1, t2);
        if (rand.NextSingle() < prob)
        {
            Console.WriteLine("\t" + t1.Name + " - " + t2.Name + " (" + scoreW + ":" + scoreL + ")");
            return t1;
        }
        else
        {
            Console.WriteLine("\t" + t1.Name + " - " + t2.Name + " (" + scoreL + ":" + scoreW + ")");
            return t2;
        }
    }


}