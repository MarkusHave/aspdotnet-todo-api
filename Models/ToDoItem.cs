using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace TodoApi.Models
{
  public class TodoItem
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("Name")]
    [JsonProperty("TaskName")]
    public string Name { get; set; }

    [BsonElement("IsComplete")]
    public bool IsComplete { get; set; }
  }
}