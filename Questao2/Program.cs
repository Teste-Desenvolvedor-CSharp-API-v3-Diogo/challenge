using FootballGoalsConsoleApp.Services;

var httpClient = new HttpClient();
var service = new FootballService(httpClient);

Console.WriteLine("=== Football Goals Calculator ===");

while (true)
{
    Console.Write("\nEnter team name (or type 'exit' to quit): ");
    string team = Console.ReadLine()?.Trim();

    if (string.IsNullOrEmpty(team) || team.ToLower() == "exit")
        break;

    Console.Write("Enter year: ");
    if (!int.TryParse(Console.ReadLine(), out int year))
    {
        Console.WriteLine("Invalid year. Please try again.");
        continue;
    }

    try
    {
        int goals = await service.GetTotalGoalsAsync(team, year);
        Console.WriteLine($"Team {team} scored {goals} goals in {year}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}

Console.WriteLine("\nGoodbye!");
