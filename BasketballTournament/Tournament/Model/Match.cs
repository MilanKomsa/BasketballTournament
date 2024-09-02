public class Match
{
    DateTime date;
    string opponent;
    string result;

    public Match(DateTime date, string opponent, string result)
    {
        this.date = date;
        this.opponent = opponent;
        this.result = result;
    }

    public DateTime Date { get => date; set => date = value; }
    public string Opponent { get => opponent; set => opponent = value; }
    public string Result { get => result; set => result = value; }
}