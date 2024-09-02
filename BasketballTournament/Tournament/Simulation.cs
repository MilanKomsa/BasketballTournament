
public class Simulation
{
    static string filePath1 = "File/groups.json";
    static string filePath2 = "File/exibitions.json";

    public static void Run()
    {
        List<Group> groups = Simulation.GetData();
        Tournament tourney = new Tournament(groups);
        tourney.Start();
    }

    static List<Group> GetData()
    {
        JSONGroupParser jgp = new JSONGroupParser(new FileHandler(filePath1)/*, new JSONExhibitionsParser(new FileHandler(filePath2))*/);
        List<Group> groups = jgp.ExtractGroups();
        JSONExhibitionsParser jep = new JSONExhibitionsParser(new FileHandler(filePath2));
        foreach (var group in groups)
        {
            foreach (var team in group.Teams)
            {
                team.Matches = jep.ParseTeamResults(team.IsoCode);
            }
        }

        return groups;
    }
}