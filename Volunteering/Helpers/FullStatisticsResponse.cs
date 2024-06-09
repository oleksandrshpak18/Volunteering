namespace Volunteering.Helpers
{
    public class FullStatisticsResponse
    {
        public NumberStatistics NumberStatistics { get; set; }
        public List<CategoryStatistics> CategoriesStatistics { get; set; }
    }

    public class NumberStatistics
    {
        public decimal AccumulatedTotal { get; set; }
        public int DonationsCount { get; set; }
        public decimal DonationsAverage {  get; set; }
    }

    public class CategoryStatistics
    {
        public string Category { get; set; }
        public decimal Accumulated { get; set; }
        public List<SubcategoryStatistics> Subcategories { get; set; }

    }

    public class SubcategoryStatistics
    {
        public string Subcategory { get; set; }
        public decimal Accumulated { get; set; }
    }
}
