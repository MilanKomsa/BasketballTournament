// See https://aka.ms/new-console-template for more information
using System.Text.Json;

class Program
{
    static void Main()
    {
        try
        {
            Simulation.Run();
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
        }
    }
}
