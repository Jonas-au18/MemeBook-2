using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MemeBook.Models;
using MongoDB.Driver;

namespace MemeBook.Services
{
    public class PostService
    {
        private readonly IMongoCollection<Post> _posts;
        private UserService _user;

        public PostService(UserService user)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("MemeBook");

            _posts = database.GetCollection<Post>("Posts");
            _user = user;
        }

        //Pure seeding purpose
        public List<Post> Get()
        {
            return _posts.Find(m => true).ToList();
        }

        public Post GetPostById(string id)
        {
            return _posts.Find(m => m.Post_ID == id).FirstOrDefault();;
        }

        public List<Post> GetPostByUserId(string id)
        {
            var Posts = _posts.Find(m => m.Owner_ID == id).ToList();
            
            return Posts;
        }

        public List<Post> GetPostByCircle(Circles circles)
        {
            return _posts.Find(m => m.Circle_ID == circles.Circle_ID).ToList();
        }
        public void CreatePost(Post post)
        {
            Console.WriteLine(post.Owner_ID);
            _posts.InsertOne(post);
            var user = _user.GetById(post.Owner_ID);
            user.Post_ID.Add(post.Post_ID);
            _user.UpdateUser(user);
        }

        public void DeletePost(string id)
        {
            var post = _posts.Find(m => m.Post_ID == id).FirstOrDefault();
            _posts.DeleteOne(m => m.Post_ID == id);
            var user = _user.GetById(post.Owner_ID);
            user.Post_ID.Remove(id);
            _user.UpdateUser(user);

        }

        public void CreateComment(string postID, Comment comment)
        {

            var post = _posts.Find(m => m.Post_ID == postID).FirstOrDefault();
            post.Comments.Add(comment);
            _posts.ReplaceOne(m => m.Post_ID == post.Post_ID,post);
        }

        public void DeleteComment(int id, Post post)
        {
            post.Comments.RemoveAt(id);
            _posts.ReplaceOne(post.Post_ID, post);
        }

    }
}
