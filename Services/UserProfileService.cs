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
            // Hash the password before storing
            userProfile.Password = BCrypt.Net.BCrypt.HashPassword(userProfile.Password);
            await _userProfileCollection.InsertOneAsync(userProfile);
        }

        // Authenticate user
        public async Task<UserProfile?> AuthenticateUser(string username, string password)
        {
            var userProfile = await _userProfileCollection.Find(l => l.Username == username).FirstOrDefaultAsync();
            if (userProfile != null && BCrypt.Net.BCrypt.Verify(password, userProfile.Password))
            {
                return userProfile; // Password matches
            }
            return null; // Invalid credentials
        }

        public async Task EditUserProfile(string id, UserProfile userProfile)
        {
            var filter = new BsonDocument("_id", new ObjectId(id));
            await _userProfileCollection.ReplaceOneAsync(filter, userProfile);
            return;
        }

        public async Task<bool> ChangePassword(string username, string oldPassword, string newPassword)
        {
            var userProfile = await _userProfileCollection.Find(u => u.Username == username).FirstOrDefaultAsync();
            if (userProfile == null)
            {
                return false; // User not found
            }

            // Verify the old password
            if (!BCrypt.Net.BCrypt.Verify(oldPassword, userProfile.Password))
            {
                return false; // Old password doesn't match
            }

            // Update with the new password (hash it before storing)
            userProfile.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _userProfileCollection.ReplaceOneAsync(u => u.Id == userProfile.Id, userProfile);

            return true; // Password successfully changed
        }

        public async Task DeleteUserProfile(string id)
        {
            await _userProfileCollection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
            return;
        }
    }
}
