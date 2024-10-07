using HydeBack.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HydeBack.Services
{
    public class AttendeeService
    {
        private readonly IMongoCollection<Attendee> _attendeeCollection;
        public AttendeeService(IOptions<MongoDBSettings> mongodbSettings)
        {
            MongoClient client = new MongoClient(mongodbSettings.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(mongodbSettings.Value.DatabaseName);
            _attendeeCollection = database.GetCollection<Attendee>(mongodbSettings.Value.AttendeeCollectionName);
        }

        public async Task<List<Attendee>> GetAttendee()
        {

            return await _attendeeCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Attendee> GetAttendeeById(string id)
        {
            return await _attendeeCollection.Find(attendee => attendee.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddAttendee(Attendee attendee)
        {
            await _attendeeCollection.InsertOneAsync(attendee);
            return;
        }

        public async Task EditAttendee(string id, Attendee attendee)
        {
            var filter = new BsonDocument("_id", new ObjectId(id));
            await _attendeeCollection.ReplaceOneAsync(filter, attendee);
            return;
        }

        public async Task DeleteAttendee(string id)
        {
            await _attendeeCollection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
            return;
        }
    }
}
