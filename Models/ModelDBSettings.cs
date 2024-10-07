namespace HydeBack.Models
{
    public class ModelDBSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string ItemsCollectionName { get; set; } = null!;
        public string LoginsCollectionName { get; set; } = null!;
        public string CategoryCollectionName { get; set; } = null!;
        public string SubCategoryCollectionName { get; set; } = null!;
        public string UserProfileCollectionName { get; set; } = null!;
        public string AttendeeCollectionName { get; set; } = null!;
        public string OrderCollectionName { get; set; } = null!;
    }
}
