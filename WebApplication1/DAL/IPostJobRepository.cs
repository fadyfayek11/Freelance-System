using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
    public interface IPostJobRepository
    {
        IEnumerable<PostJob> GetAllPosts();
        IEnumerable<PostJob> GetAllPostsOfUser(string userId);
        PostJob GetPostJobById(int? PostId);
        void CreatePost(PostJob post);
        void DeltePost(PostJob post);
        void EditPost(PostJob Post);
        void Save();
    }
}