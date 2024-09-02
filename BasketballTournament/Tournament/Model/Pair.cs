enum Stage
{
    Hat,
    Quarter,
    Semi,
    Final
}

class Pair
{
    Stage stage;
    Team t1;
    Team t2;

    public Pair(Stage stage, Team t1, Team t2)
    {
        this.stage = stage;
        this.t1 = t1;
        this.t2 = t2;
    }

    public Team T1 { get => t1; set => t1 = value; }
    public Team T2 { get => t2; set => t2 = value; }
    internal Stage Stage { get => stage; set => stage = value; }
}