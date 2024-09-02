public interface IGroupSimulator
{
    //Plays group phase match
    void PlayMatch(Group g, Team t1, Team t2);
    //Sorts standing table
    void SortStandings(List<Group> groups);
    //Creates list of 8 teams that go to hatdraw phase
    List<Team> FormHatRanking(List<Group> groups);
}