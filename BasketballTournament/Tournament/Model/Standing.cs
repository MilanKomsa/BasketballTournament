public class Standing : IComparable<Standing>
{
    //memento undo would be great here :)
    #region fields
    int rank;
    string name;
    int wins;
    int losses;
    int points;
    int scored;
    int received;
    int scoreDiff;
    #endregion
    public Standing(int rank, string name, int wins, int losses, int points, int scored, int received, int scoreDiff)
    {
        this.rank = rank;
        this.name = name;
        this.wins = wins;
        this.losses = losses;
        this.points = points;
        this.scored = scored;
        this.received = received;
        this.scoreDiff = scoreDiff;
    }

    public Standing(Standing standing, int scoredDeduct, int receivedDeduct, int diffDeduct)
    {
        this.rank = standing.rank;
        this.name = standing.name;
        this.wins = standing.wins;
        this.losses = standing.losses;
        this.points = standing.points;
        this.scored = standing.scored - scoredDeduct;
        this.received = standing.received - receivedDeduct;
        this.scoreDiff = standing.scoreDiff - diffDeduct;
    }

    public int CompareTo(Standing other)
    {
        if (other.Points.CompareTo(this.Points) != 0)
        {
            return other.Points.CompareTo(this.Points);
        }
        else if (other.ScoreDiff.CompareTo(this.ScoreDiff) != 0)
        {
            return other.ScoreDiff.CompareTo(this.ScoreDiff);
        }
        else if (other.Scored.CompareTo(this.Scored) != 0)
        {
            return other.Scored.CompareTo(this.Scored);
        }
        else
        {
            return other.Rank.CompareTo(this.Rank);
        }
    }
    #region standing group updates
    public void UpdateStandingWinner(int scored, int received)
    {
        this.Wins += 1;
        this.Points += 2;
        this.Scored += scored;
        this.Received += received;
        this.ScoreDiff += scored - received;
    }

    public void UpdateStandingLosser(int scored, int received)
    {
        this.Losses += 1;
        this.Points += 1;
        this.Scored += scored;
        this.Received += received;
        this.ScoreDiff += scored - received;
    }

    public void UpdateStandingWinnerBySurrender()
    {
        this.Wins += 1;
        this.Points += 2;
        this.Scored += 20;
        this.Received += 0;
        this.ScoreDiff += 20;
    }

    public void UpdateStandingLosserBySurrender()
    {
        this.Losses += 1;
        this.Points += 0;
        this.Scored += 0;
        this.Received += 20;
        this.ScoreDiff += -20;
    }
    #endregion

    #region properties
    public int Rank { get => rank; set => rank = value; }
    public string Name { get => name; set => name = value; }
    public int Wins { get => wins; set => wins = value; }
    public int Losses { get => losses; set => losses = value; }
    public int Points { get => points; set => points = value; }
    public int Scored { get => scored; set => scored = value; }
    public int Received { get => received; set => received = value; }
    public int ScoreDiff { get => scoreDiff; set => scoreDiff = value; }
    #endregion
}