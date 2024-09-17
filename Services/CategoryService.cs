using HydeBack.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HydeBack.Services
{
    public class CategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        public CategoryService(IOptions<MongoDBSettings> mongodbSettings)
        {
            MongoClient client = new MongoClient(mongodbSettings.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(mongodbSettings.Value.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(mongodbSettings.Value.CategoryCollectionName);
        }

        public async Task<List<Category>> GetCategory()
        {

            return await _categoryCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Category> GetCategoryById(string id)
        {
            return await _categoryCollection.Find(category => category.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddCategory(Category category)
        {
            await _categoryCollection.InsertOneAsync(category);
            return;
        }

        public async Task EditCategory(string id, Category category)
        {
            var filter = new BsonDocument("_id", new ObjectId(id));
            await _categoryCollection.ReplaceOneAsync(filter, category);
            return;
        }

        public async Task DeleteCategory(string id)
        {
            await _categoryCollection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
            return;
        }
    }
}
