using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookshelfManager.Models
{
   public class Book
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Title")]
    public string? Title { get; set; }

    [BsonElement("Author")]
    public string? Author { get; set; }

    [BsonElement("PublicationYear")]
    public int PublicationYear { get; set; }

    [BsonElement("ISBN")]
    public string? ISBN { get; set; }

    [BsonElement("Genre")]
    public string? Genre { get; set; }
}

}
