using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace CSharpWebApi.Models
{
    public class Product
    {
        [BsonId]
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;
        [BsonElement("price")]
        public decimal Price { get; set; }
    }
}
