using HydeBack.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HydeBack.Services
{
    public class OrderService
    {
        private readonly IMongoCollection<Order> _ordersCollection;
        private readonly IMongoCollection<Order> _itemsCollection;

        public OrderService(IOptions<MongoDBSettings> mongodbSettings)
        {
            MongoClient client = new MongoClient(mongodbSettings.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(mongodbSettings.Value.DatabaseName);
            _ordersCollection = database.GetCollection<Order>(mongodbSettings.Value.OrderCollectionName);
            _itemsCollection = database.GetCollection<Order>(mongodbSettings.Value.ItemsCollectionName);
        }

        public async Task<List<Order>> GetOrders()
        {

            return await _ordersCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Order> GetOrderById(string id)
        {
            return await _ordersCollection.Find(order => order.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddOrders(Order order)
        {
            await _ordersCollection.InsertOneAsync(order);
            return;
        }

        /*public async Task AddOrders(Order order)
        {
            if (order.Items == null || !order.Items.Any())
            {
                throw new Exception("Order must contain at least one item.");
            }

            decimal totalPrice = 0;
            foreach (var item in order.Items)
            {
                totalPrice += item.Price * item.Quantity;
            }

            order.TotalPrice = totalPrice.ToString();
            await _ordersCollection.InsertOneAsync(order);
        }*/

        public async Task EditOrders(string id, Order order)
        {
            var filter = new BsonDocument("_id", new ObjectId(id));
            await _ordersCollection.ReplaceOneAsync(filter, order);
            return;
        }

        public async Task DeleteOrders(string id)
        {
            await _ordersCollection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
            return;
        }
    }
}
