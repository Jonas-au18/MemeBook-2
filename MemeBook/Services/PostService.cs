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

        public PostService()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("MemeBook");

            _posts = database.GetCollection<Post>("Posts");
            _user = new UserService();
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

        public List<Post> GetPostByCircle(Circles circles)
        {
            return _posts.Find(m => m.Circles == circles).ToList();
        }
        public void CreatePost(string id, string content, Circles circle)
        {
            Post post = new Post
            {
                Circles = circle,
                Content = content,
                Owner_ID = id
            };
            _posts.InsertOne(post);
            var user = _user.GetById(id);
            user.Post_ID.Add(post.Post_ID);
            _user.UpdateUser(user);
        }

        public void DeletePost(string id)
        {
            _posts.DeleteOne(m => m.Post_ID == id);
        }

        public void CreateComment(Post post, string content, User user)
        {
            Post myPost = post;
            myPost.Comments.Add(new Comment
            {
                Owner_ID = user.User_ID,
                Content = content
            });
            _posts.ReplaceOne(m => m.Post_ID == myPost.Owner_ID,myPost);
        }

        public void DeleteComment(int id, Post post)
        {
            post.Comments.RemoveAt(id);
            _posts.ReplaceOne(post.Post_ID, post);
        }

        public void KillAllPosts()
        {
            _posts.DeleteMany(m => m.Post_ID != null);
        }
    }
}
