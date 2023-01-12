using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MainApp.DTO;

public class MusicDTO
{
    // To keep the example simple, the base music model will be used in here 
    [BsonRepresentation(BsonType.String)]
    [BsonElement("id")]
    public string Id { get; set; }
    [BsonElement("title")]
    [Required]
    public string Title { get; set; }
    [BsonElement("releaseDate")]
    public DateTime ReleaseDate { get; set; }
    [BsonElement("artist")]
    public string Artist { get; set; }
    [BsonElement("rate")]
    public int Rate { get; set; }
}