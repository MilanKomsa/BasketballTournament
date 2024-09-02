class SetupPhase : ISetupPhase
{

    ISetupSimulator simulator;
    public SetupPhase(ISetupSimulator simulator)
    {
        this.simulator = simulator;
    }

    public void Start(List<Group> groups)
    {
        simulator.ResetStandings(groups);
    }
}