﻿using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MemeBook.Models
{
    public class Circles
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Circle_ID { get; set; }
        [BsonElement("Members")] public List<User> users { get; set; } = new List<User>();
    }
}