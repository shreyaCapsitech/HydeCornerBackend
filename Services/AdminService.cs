using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Reflection;
using System.Threading.Tasks;
using HydeBack.Models;


namespace HydeBack.Services
{
    public class AdminService
    {
        private readonly IMongoCollection<Admin> _adminsCollection;
        public AdminService(IOptions<MongoDBSettings> mongodbSettings)
        {
            MongoClient client = new MongoClient(mongodbSettings.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(mongodbSettings.Value.DatabaseName);
            _adminsCollection = database.GetCollection<Admin>(mongodbSettings.Value.AdminsCollectionName);
        }

        public async Task<List<Admin>> GetAdmins()
        {

            return await _adminsCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Admin> GetAdminById(string id)
        {
            return await _adminsCollection.Find(admin => admin.Id == id).FirstOrDefaultAsync();
        }

        public async System.Threading.Tasks.Task AddAdmins(Admin admin)
        {
            await _adminsCollection.InsertOneAsync(admin);
            return;
        }

        public async System.Threading.Tasks.Task EditAdmins(string id, Admin admin)
        {
            var filter = new BsonDocument("_id", new ObjectId(id));
            await _adminsCollection.ReplaceOneAsync(filter, admin);
            return;
        }

        public async System.Threading.Tasks.Task DeleteAdmins(string id)
        {
            await _adminsCollection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
            return;
        }
    }
}
