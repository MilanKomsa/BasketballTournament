interface IDrawSimulator
{
    //Distributes teams to their hats
    List<Pair> FormulateHats(List<Team> teams);
    //Creates pairs for quarter finals
    List<Pair> FormulatePairs(List<Pair> hats);
}