using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MemeBook.Controller;
using MemeBook.Models;
using MemeBook.Services;

namespace MemeBook.Queries
{
    public class Create
    {
        private LayoutControl _layout;
        private PostService _pService;
        public Create(LayoutControl layout, PostService pServ)
        {
            _layout = layout;
            _pService = pServ;
        }
        public void CreatePost(Circles circle, string user)
        {
            _layout.boxLine();
            Console.WriteLine("Enter the data you want to post");
            _layout.boxLine();
            string content = Console.ReadLine();
            _layout.boxLine();
            Console.WriteLine("Is the post public? [Y/N]");
            bool isPublic;
            if (Console.ReadLine() == "y")
            {
                isPublic = true;
            }
            else
            {
                isPublic = false;
            }
            Console.WriteLine(user);
            Post myPost = new Post()
            {
                Owner_ID = user,
                Content = content,
                isPublic = isPublic,
                Circle_ID = circle.Circle_ID
            };
            _pService.CreatePost(myPost);
        }

        public void DeletePost(Circles circle)
        {
            _layout.boxLine();
            Console.WriteLine("Enter post you want to delete");
            _layout.boxLine();
            var posts = _pService.GetPostByCircle(circle);
            int index = Convert.ToInt32(Console.ReadLine());
            _pService.DeletePost(posts[index].Post_ID);
        }

        public void PostImage()
        {
            _layout.boxLine();
            Console.WriteLine("Upload the picture you want to post, by writing the file path. Max size 8 mb.");
            _layout.boxLine();
            string path = Console.ReadLine();

        }

        public void CreateComment(Post post, string user)
        {
            _layout.boxLine();
            Console.WriteLine("Enter the comment you want to add");
            _layout.boxLine();
            string comment = Console.ReadLine();
            Comment newcomment = new Comment();
            newcomment.Owner_ID = user;
            newcomment.Content = comment;
            post.Comments.Add(newcomment);
            _pService.CreateComment(post.Post_ID,newcomment);
        }

        public void DeleteComment(int index, Post post)
        {
            _layout.boxLine();
            Console.WriteLine("Enter the number comment you want to remove");
            _layout.boxLine();
            int ind = Convert.ToInt32(Console.ReadLine());
            _pService.DeleteComment(ind,post);
        }
    }
}
