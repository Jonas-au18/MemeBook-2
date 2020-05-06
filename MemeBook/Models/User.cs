using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MemeBook.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string User_ID { get; set; }

        [BsonElement("Fullname")]public string Fullname { get; set; }
        [BsonElement("Age")] public int Age { get; set; } = 0;
        [BsonElement("Gender")] public char gender { get; set; } = ' ';
        [BsonElement("Post_ID")] public List<string> Post_ID { get; set; } = new List<string>();
        [BsonElement("Logged_in")] public bool Logged_in { get; set; } = false;
        [BsonElement("Blocked")]public List<string> Blocked { get; set; } = new List<string>();
        [BsonElement("Circle")]public Circles PersonalCircle { get; set; }
        [BsonElement("Followed")]public List<string> Following { get; set;} = new List<string>();
    }
}