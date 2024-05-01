namespace Volunteering.Helpers
{
    public class StatisticsResponse
    {
        public decimal ?Accumulated { get; set; }
        public int RegisteredUsers { get; set; }
        public int FinishedCampaigns { get; set; }
        public int Donations {  get; set; }
    }
}
