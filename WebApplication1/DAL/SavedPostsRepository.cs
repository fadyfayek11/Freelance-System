using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
    public class SavedPostsRepository : ISavedPostsRepository
    {
        private readonly ApplicationDbContext _db;
        public SavedPostsRepository(ApplicationDbContext Db)
        {
            _db = Db;
        }

        public void SavePostAtMyPage(Saved savedPost)
        {
            _db.Saveds.Add(savedPost);
        }
        public void DeleteSavedPost(Saved savedPost)
        {
            _db.Saveds.Remove(savedPost);
            
        }
        public async System.Threading.Tasks.Task<bool> CkeckIfthePostSavedBeforeAsync(int postId, string userId)
        {
            return await _db.Saveds.FirstOrDefaultAsync(s => s.IdOfThePost == postId && s.IdOfTheUser == userId) != null;
        }

        public Saved GetSavedsById(int? SavedPostId)
        {
            return _db.Saveds.FirstOrDefault(s => s.IdOfThePost == SavedPostId);
        }

        public async Task SaveChangeAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void SaveChange()
        {
            _db.SaveChanges();
        }
    }
}