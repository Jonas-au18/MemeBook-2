using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MemeBook.Models
{
    public class Login
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Login_id { get; set; }

        public string User_id { get; set; } = "";
        [BsonElement("Username")] public string Username { get; set; } ="";
        [BsonElement("Password")] public string Password { get; set; } = "";

    }
}