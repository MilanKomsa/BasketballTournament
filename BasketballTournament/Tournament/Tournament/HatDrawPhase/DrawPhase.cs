class DrawPhase : IDrawPhase
{
    IDrawSimulator simulator;
    public DrawPhase(IDrawSimulator simulator)
    {
        this.simulator = simulator;
    }

    public List<Pair> Start(List<Team> teams)
    {
        this.PrintOutStart(teams);
        List<Pair> hats = simulator.FormulateHats(teams);
        this.PrintHats(hats);
        List<Pair> quarterPairs = simulator.FormulatePairs(hats);
        this.PrintPairs(quarterPairs);

        return quarterPairs;
    }

    void PrintOutStart(List<Team> teams)
    {
        Console.WriteLine("\nTimovi u šeširu:");
        foreach (var team in teams)
        {
            Console.WriteLine("\t" + team.Name);
        }
    }

    void PrintHats(List<Pair> hats)
    {
        Console.WriteLine("\nŠeširi:");
        int offset = 0;
        foreach (var hat in hats)
        {
            Console.WriteLine("\tŠešir " + Convert.ToChar(68 + offset++));
            Console.WriteLine("\t\t" + hat.T1.Name);
            Console.WriteLine("\t\t" + hat.T2.Name);
        }
    }

    void PrintPairs(List<Pair> pairs)
    {
        int cnt = 0;
        Console.WriteLine("\nEliminaciona faza:");
        foreach (var pair in pairs)
        {
            cnt++;
            Console.WriteLine("\t" + pair.T1.Name + " - " + pair.T2.Name);
            if (cnt == 2)
            {
                Console.WriteLine();
            }
        }
    }


}