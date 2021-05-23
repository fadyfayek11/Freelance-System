using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
    public class PostJobRepository: IPostJobRepository
    {
        private readonly ApplicationDbContext _db;
        public PostJobRepository(ApplicationDbContext Db)
        {
            _db = Db;
        }

        public IEnumerable<PostJob> GetAllPostsOfUser(string userId)
        {
            return _db.PostJobs.Where(p => p.UserId == userId).ToList();
        }
        public void CreatePost(PostJob post)
        {
            _db.PostJobs.Add(post);
        }

        public void DeltePost(PostJob post)
        {            
            _db.PostJobs.Remove(post);
        }

        public void EditPost(PostJob post)
        {
            _db.Entry(post).State = EntityState.Modified;
        }

        public IEnumerable<PostJob> GetAllPosts()
        {
            return  _db.PostJobs.ToList();
        }

        public PostJob GetPostJobById(int? PostId)
        {
            return _db.PostJobs.FirstOrDefault(p => p.Id == PostId);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}