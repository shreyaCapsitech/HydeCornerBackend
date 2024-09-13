using MongoDB.Driver;

namespace HydeBack.Services
{
    public class MongoDBSettings
    {
        public string? ConnectionString { get; set; }
        public string? DatabaseName { get; set; }
        public string? ItemsCollectionName { get; set; }
        public string? LoginsCollectionName { get; set; }
    }
}
