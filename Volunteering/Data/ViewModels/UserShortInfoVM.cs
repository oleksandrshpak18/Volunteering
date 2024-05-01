namespace Volunteering.Data.ViewModels
{
    public class UserShortInfoVM
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public decimal Accumulated {  get; set; }
        public int ReportCount { get; set; }
    }
}
