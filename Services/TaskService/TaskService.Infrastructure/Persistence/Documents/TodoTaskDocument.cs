using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TaskService.Infrastructure.Persistence.Documents
{
    public class TodoTaskDocument
    {
        [BsonId]
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }

        [BsonElement("title")]
        public string TaskTitle { get; set; } = null!;

        [BsonElement("isCompleted")]
        public bool IsCompleted { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
