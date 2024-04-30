namespace Volunteering.Data.ViewModels
{
    public class CampaignPriorityVm
    {
        public Guid? CampaignPriorityId { get; set; }
        public byte PriorityValue { get; set; }
        public string PriorityDescription { get; set; } = null!;
    }
}
