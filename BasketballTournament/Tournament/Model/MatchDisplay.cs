public class MatchDisplay
{
    string home;
    string visitor;
    string result;

    public MatchDisplay(string home, string visitor, string result)
    {
        this.home = home;
        this.visitor = visitor;
        this.result = result;
    }

    public string Home { get => home; set => home = value; }
    public string Visitor { get => visitor; set => visitor = value; }
    public string Result { get => result; set => result = value; }
}