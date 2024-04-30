namespace Volunteering.Helpers
{
    public class CampaignStatusUpdateRequest
    {
        public Guid StatusId { get; set; }
        public string NewStatus { get; set; }
    }
}
