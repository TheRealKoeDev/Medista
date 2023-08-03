// Test Header

using LiteDB;

namespace AppData.Models
{
    public abstract class Model
    {
        [BsonId]
        public Guid Id { get; set; }
    }
}