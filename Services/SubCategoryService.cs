using HydeBack.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HydeBack.Services
{
    public class SubCategoryService
    {
        private readonly IMongoCollection<SubCategory> _subCategoryCollection;
        public SubCategoryService(IOptions<MongoDBSettings> mongodbSettings)
        {
            MongoClient client = new MongoClient(mongodbSettings.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(mongodbSettings.Value.DatabaseName);
            _subCategoryCollection = database.GetCollection<SubCategory>(mongodbSettings.Value.SubCategoryCollectionName);
        }

        public async Task<List<SubCategory>> GetSubCategory()
        {

            return await _subCategoryCollection.Find(_ => true).ToListAsync();
        }

        public async Task<SubCategory> GetSubCategoryById(string id)
        {
            return await _subCategoryCollection.Find(subCategory => subCategory.Id == id).FirstOrDefaultAsync();
        }

        public async System.Threading.Tasks.Task AddSubCategory(SubCategory subCategory)
        {
            await _subCategoryCollection.InsertOneAsync(subCategory);
            return;
        }

        public async System.Threading.Tasks.Task EditSubCategory(string id, SubCategory subCategory)
        {
            var filter = new BsonDocument("_id", new ObjectId(id));
            await _subCategoryCollection.ReplaceOneAsync(filter, subCategory);
            return;
        }

        public async System.Threading.Tasks.Task DeleteSubCategory(string id)
        {
            await _subCategoryCollection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
            return;
        }
    }
}
