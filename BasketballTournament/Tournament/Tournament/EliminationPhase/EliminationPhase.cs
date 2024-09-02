
class EliminationPhase : IEliminationPhase
{
    IEliminationSimulator simulator;
    public EliminationPhase(IEliminationSimulator simulator)
    {
        this.simulator = simulator;
    }

    public void Start(List<Pair> quarterPairs)
    {
        List<Pair> semiPairs = simulator.PlayQuarterFinals(quarterPairs);
        List<Pair> finalPairs = simulator.PlaySemiFinals(semiPairs);
        simulator.PlayFinals(finalPairs);
    }
}