
using System.Reflection.Emit;

class Helper : IHelper
{
    int diffWeight;
    int rankWeight;

    public Helper(int diffWeight = 1, int rankWeight = 1)
    {
        this.DiffWeight = diffWeight;
        this.RankWeight = rankWeight;
    }

    public int DiffWeight { get => diffWeight; set => diffWeight = value; }
    public int RankWeight { get => rankWeight; set => rankWeight = value; }

    public double CalculateProbabilities(Team t1, Team t2)
    {
        double diff1 = GetLastTwoDifference(t1.Matches) / 6; // |maxdiff| = 30, /6 => +-5 in rank score 
        double diff2 = GetLastTwoDifference(t2.Matches) / 6;

        double formRank1 = -diff1 * diffWeight + t1.Rank * rankWeight;
        double formRank2 = -diff2 * diffWeight + t2.Rank * rankWeight;

        //instead of recursive calculations of residuals, with all my might, i stop headache !! 
        //Americi na stetu ide svakako :)
        if (formRank1 < 1)
        {
            formRank1 = 1;
        }

        if (formRank2 < 1)
        {
            formRank2 = 1;
        }

        double range = formRank1 + formRank2;

        return 1 - (formRank1 / range);
    }


    double ExtractDifference(string result)
    {
        int[] points = Array.ConvertAll(result.Split('-'), int.Parse);
        return (double)(points[0] - points[1]);
    }

    double GetLastTwoDifference(List<Match> matches)
    {
        double result = 0;

        for (int i = 0; i < 2; i++)
        {
            result += 1 / Math.Pow(2, i + 1) * ExtractDifference(matches[matches.Count - i - 1].Result);//time dependant series, more recent score is more important 
        }

        return result;
    }
}