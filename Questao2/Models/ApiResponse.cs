namespace FootballGoalsConsoleApp.Models
{
    public class ApiResponse
    {
        public int Page { get; set; }
        public int Total_Pages { get; set; }
        public List<MatchResult> Data { get; set; }
    }
}