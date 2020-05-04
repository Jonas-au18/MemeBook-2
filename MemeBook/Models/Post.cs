using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MemeBook.Models
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Post_ID { get; set; }

        [BsonElement("Owner")] public string Owner_ID { get; set; } = "";
        [BsonElement("Content")] public string Content { get; set; } = "";
        [BsonElement("Circle")]public string Circle_ID { get; set; }
        [BsonElement("Comments")]public List<Comment> Comments { get; set; } = new List<Comment>();
        [BsonElement("Likes")] public int Likes { get; set; } = 0;
        [BsonElement("Dislikes")] public int Dislikes { get; set; } = 0;
        [BsonElement("Public")]public bool isPublic { get; set; } = true;
        [BsonElement("Created")] public string date = DateTime.Now.ToShortDateString();
    }
}

