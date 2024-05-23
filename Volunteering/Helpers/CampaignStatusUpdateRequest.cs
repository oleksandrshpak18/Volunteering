namespace Volunteering.Helpers
{
    public class CampaignStatusUpdateRequest
    {
        public Guid CampaignId { get; set; }
        public string NewStatus { get; set; }
    }
}
