class DrawSimulator : IDrawSimulator
{
    public List<Pair> FormulateHats(List<Team> teams)
    {
        List<Pair> hats = new List<Pair>();
        for (int i = 0; i < 4; i++)
        {
            hats.Add(new Pair(Stage.Hat, teams[i * 2], teams[i * 2 + 1]));
        }

        return hats;
    }

    public List<Pair> FormulatePairs(List<Pair> hats)
    {
        List<Pair> quarterPairs = new List<Pair>(new Pair[4]);

        int position = 0;
        foreach (var pair in AdvanceToQuarter(hats[0], hats[3]))
        {
            quarterPairs[position * 2] = pair;
            position++;
        }
        position = 0;
        foreach (var pair in AdvanceToQuarter(hats[1], hats[2]))
        {
            quarterPairs[position * 2 + 1] = pair;
            position++;
        }

        return quarterPairs;
    }

    List<Pair> AdvanceToQuarter(Pair hat1, Pair hat2)
    {
        Random rand = new Random();
        List<Pair> pairs = new List<Pair>();

        bool choice1 = rand.Next() < 0.5 ? true : false;
        Team t1 = choice1 ? hat1.T1 : hat1.T2;
        bool choice2 = rand.Next() < 0.5 ? true : false;
        Team t2 = choice2 ? hat2.T1 : hat2.T2;
        Team t3 = choice2 ? hat2.T2 : hat2.T1;

        if (t1.Matches.TakeLast(3).Any(m => m.Opponent == t2.IsoCode))
        {
            pairs.Add(new Pair(Stage.Quarter, t1, t3));
            t1 = !choice1 ? hat1.T1 : hat1.T2;
            pairs.Add(new Pair(Stage.Quarter, t2, t1));
        }
        else
        {
            if ((!choice1 ? hat1.T1 : hat1.T2).Matches.TakeLast(3).Any(m => m.Opponent == t3.IsoCode))
            {
                pairs.Add(new Pair(Stage.Quarter, t1, t3));
                t1 = !choice1 ? hat1.T1 : hat1.T2;
                pairs.Add(new Pair(Stage.Quarter, t2, t1));
            }
            else
            {
                pairs.Add(new Pair(Stage.Quarter, t1, t2));
                t1 = !choice1 ? hat1.T1 : hat1.T2;
                pairs.Add(new Pair(Stage.Quarter, t3, t1));
            }
        }

        return pairs;
    }
}