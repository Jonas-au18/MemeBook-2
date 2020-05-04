using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MemeBook.Models
{
    public class Comment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Comment_ID { get; set; }

        public string Owner_ID { get; set; } = "";
        [BsonElement("Content")] public string Content { get; set; } ="";
        [BsonElement("Likes")] public int Likes { get; set; } = 0;
        [BsonElement("Dislike")] public int Dislikes { get; set; } = 0;
        [BsonElement("Created")] public string date = DateTime.Now.ToShortDateString();
    }
}
