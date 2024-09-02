interface IEliminationSimulator
{
    //Plays quarter finals and returns the semi finals participants
    List<Pair> PlayQuarterFinals(List<Pair> quarterPairs);
    //Plays semi finals and returns the finals participants
    List<Pair> PlaySemiFinals(List<Pair> quarterPairs);
    //Plays finals and prints out winners
    void PlayFinals(List<Pair> finalPairs);
}