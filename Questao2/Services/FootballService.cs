using System.Text.Json;
using FootballGoalsConsoleApp.Models;

namespace FootballGoalsConsoleApp.Services
{
    public class FootballService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://jsonmock.hackerrank.com/api/football_matches";

        public FootballService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetTotalGoalsAsync(string team, int year)
        {
            int totalGoals = 0;
            totalGoals += await GetGoalsByTeamSide(team, year, isTeam1: true);
            totalGoals += await GetGoalsByTeamSide(team, year, isTeam1: false);

            return totalGoals;
        }
        private async Task<int> GetGoalsByTeamSide(string team, int year, bool isTeam1)
        {
            int totalGoals = 0;
            int currentPage = 1;
            int totalPages;

            string teamParam = isTeam1 ? "team1" : "team2";

            do
            {
                var url = $"{BaseUrl}?year={year}&{teamParam}={team}&page={currentPage}";
                var response = await _httpClient.GetStringAsync(url);
                var apiResponse = JsonSerializer.Deserialize<ApiResponse>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                foreach (var match in apiResponse.Data)
                {
                    int goals = isTeam1
                        ? int.Parse(match.Team1Goals)
                        : int.Parse(match.Team2Goals);
                    totalGoals += goals;
                }

                totalPages = apiResponse.Total_Pages;
                currentPage++;

            } while (currentPage <= totalPages);

            return totalGoals;
        }

    }
}