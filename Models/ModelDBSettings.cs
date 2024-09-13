namespace HydeBack.Models
{
    public class ModelDBSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string ItemsCollectionName { get; set; } = null!;
        public string LoginsCollectionName { get; set; } = null!;
    }
}
