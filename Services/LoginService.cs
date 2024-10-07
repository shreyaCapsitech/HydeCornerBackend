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

        public async Task<Login> GetLoginById(string id)
        {
            return await _loginsCollection.Find(login => login.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddLogins(Login login)
        {
            // Hash the password before storing
            login.Password = BCrypt.Net.BCrypt.HashPassword(login.Password);
            await _loginsCollection.InsertOneAsync(login);
        }

        // Authenticate user
        public async Task<Login?> AuthenticateUser(string username, string password)
        {
            var login = await _loginsCollection.Find(l => l.Username == username && l.Password == password).FirstOrDefaultAsync();
            if (login != null) //&& BCrypt.Net.BCrypt.Verify(password, login.Password))
            {
                return login; // Password matches
            }
            return null; // Invalid credentials
        }

        public async Task EditLogins(string id, Login login)
        {
            var filter = new BsonDocument("_id", new ObjectId(id));
            await _loginsCollection.ReplaceOneAsync(filter, login);
            return;
        }

        public async Task DeleteLogins(string id)
        {
            await _loginsCollection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
            return;
        }

    }
}
