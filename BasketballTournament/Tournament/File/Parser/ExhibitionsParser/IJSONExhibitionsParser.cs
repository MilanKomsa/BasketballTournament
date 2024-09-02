interface IJSONExhibitionsParser
{
    //Get team results from exhibitions
    List<Match> ParseTeamResults(string ISOCode);
}