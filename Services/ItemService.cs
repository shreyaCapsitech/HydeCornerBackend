using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Reflection;
using System.Threading.Tasks;
using HydeBack.Models;


namespace HydeBack.Services
{
    public class ItemService
    {
        private readonly IMongoCollection<Item> _itemsCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMongoCollection<SubCategory> _subCategoryCollection;

        public ItemService(IOptions<MongoDBSettings> mongodbSettings)
        {
            MongoClient client = new MongoClient(mongodbSettings.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(mongodbSettings.Value.DatabaseName);
            _itemsCollection = database.GetCollection<Item>(mongodbSettings.Value.ItemsCollectionName);
            _categoryCollection = database.GetCollection<Category>(mongodbSettings.Value.CategoryCollectionName);
            _subCategoryCollection = database.GetCollection<SubCategory>(mongodbSettings.Value.SubCategoryCollectionName);
        }

        public async Task<List<Item>> GetItems()
        {

            return await _itemsCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Item> GetItemById(string id)
        {
            return await _itemsCollection.Find(item => item.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddItems(Item item)
        {
            await _itemsCollection.InsertOneAsync(item);
            return;
        }

        public async Task EditItems(string id, Item item)
        {
            var filter = new BsonDocument("_id", new ObjectId(id));
            await _itemsCollection.ReplaceOneAsync(filter, item);
            return;
        }

        public async Task DeleteItems(string id)
        {
            await _itemsCollection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
            return;
        }
    }
}
