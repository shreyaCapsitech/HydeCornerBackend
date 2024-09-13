using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Reflection;
using System.Threading.Tasks;
using HydeBack.Models;

namespace HydeBack.Services
{
    public class LoginService
    {
        private readonly IMongoCollection<Login> _loginsCollection;
        public LoginService(IOptions<MongoDBSettings> mongodbSettings)
        {
            MongoClient client = new MongoClient(mongodbSettings.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(mongodbSettings.Value.DatabaseName);
            _loginsCollection = database.GetCollection<Login>(mongodbSettings.Value.LoginsCollectionName);
        }

        public async Task<List<Login>> GetLogins()
        {

            return await _loginsCollection.Find(_ => true).ToListAsync();
        }
        public async Task<Login?> GetLoginByUsernameAndPassword(string username, string password)
        {
            var filter = Builders<Login>.Filter.Eq(l => l.Username, username) &
                         Builders<Login>.Filter.Eq(l => l.Password, password);

            return await _loginsCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Login> GetLoginById(string id)
        {
            return await _loginsCollection.Find(login => login.Id == id).FirstOrDefaultAsync();
        }

        public async System.Threading.Tasks.Task AddLogins(Login login)
        {
            await _loginsCollection.InsertOneAsync(login);
            return;
        }

        public async System.Threading.Tasks.Task EditLogins(string id, Login login)
        {
            var filter = new BsonDocument("_id", new ObjectId(id));
            await _loginsCollection.ReplaceOneAsync(filter, login);
            return;
        }

        public async System.Threading.Tasks.Task DeleteLogins(string id)
        {
            await _loginsCollection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
            return;
        }
    }
}
