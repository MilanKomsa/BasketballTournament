interface IJSONGroupParser
{
    //Gets teams from JSON group
    List<Team> ParseGroup(string name);
    //Extracts group names from JSON document
    List<Group> ExtractGroups();
}