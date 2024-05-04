namespace Volunteering.Data.ViewModels
{
    public class CategoryVM
    {
        public string CategoryName { get; set; } = null!;
        public List<string> Subcategories { get; set; }
    }
}
