using HydeBack.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HydeBack.Services
{
    public class UserProfileService
    {
        private readonly IMongoCollection<UserProfile> _userProfileCollection;
        public UserProfileService(IOptions<MongoDBSettings> mongodbSettings)
        {
            MongoClient client = new MongoClient(mongodbSettings.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(mongodbSettings.Value.DatabaseName);
            _userProfileCollection = database.GetCollection<UserProfile>(mongodbSettings.Value.UserProfileCollectionName);
        }

        public async Task<List<UserProfile>> GetUserProfile()
        {

            return await _userProfileCollection.Find(_ => true).ToListAsync();
        }

        public async Task<UserProfile> GetUserProfileById(string id)
        {
            return await _userProfileCollection.Find(userProfile => userProfile.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddUserProfile(UserProfile userProfile)
        {
            await _userProfileCollection.InsertOneAsync(userProfile);
            return;
        }

        public async Task EditUserProfile(string id, UserProfile userProfile)
        {
            var filter = new BsonDocument("_id", new ObjectId(id));
            await _userProfileCollection.ReplaceOneAsync(filter, userProfile);
            return;
        }

        public async Task DeleteUserProfile(string id)
        {
            await _userProfileCollection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
            return;
        }
    }
}
