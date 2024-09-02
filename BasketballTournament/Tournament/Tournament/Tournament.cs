using System.Security.Principal;

public enum State
{
    Setup,
    Group,
    HatDraw,
    Elimination
}
class Tournament : ITournament
{
    State state;
    ISetupPhase setupPhase;
    IGroupPhase groupPhase;
    IDrawPhase drawPhase;
    IEliminationPhase eliminationPhase;
    List<Team> hatDrawTeams;
    List<Pair> quarterPairs;
    List<Group> groups;
    public Tournament(List<Group> participants)
    {
        this.state = State.Setup;
        this.setupPhase = new SetupPhase(new SetupSimulator());
        this.groupPhase = new GroupPhase(new GroupSimulator(new Helper()));
        this.drawPhase = new DrawPhase(new DrawSimulator());
        this.eliminationPhase = new EliminationPhase(new EliminationSimulator(new Helper()));
        this.groups = participants;
    }

    public void Start()
    {
        this.Setup();
    }
    void Setup()
    {
        if (this.State == State.Setup)
        {
            this.setupPhase.Start(Groups);
            this.state = State.Group;
            this.GroupStage();
        }
    }

    void GroupStage()
    {
        if (this.State == State.Group)
        {
            this.HatDrawTeams = this.groupPhase.Start(Groups);
            this.state = State.HatDraw;
            this.Draw();
        }
    }
    void Draw()
    {
        if (this.State == State.HatDraw)
        {
            this.QuarterPairs = this.drawPhase.Start(this.HatDrawTeams);
            this.state = State.Elimination;
            this.Elimination();
        }
    }

    void Elimination()
    {
        if (this.State == State.Elimination)
        {
            this.eliminationPhase.Start(QuarterPairs);
        }
    }
    public State State { get => state; set => state = value; }
    public List<Group> Groups { get => groups; set => groups = value; }
    public List<Team> HatDrawTeams { get => hatDrawTeams; set => hatDrawTeams = value; }
    internal List<Pair> QuarterPairs { get => quarterPairs; set => quarterPairs = value; }
}