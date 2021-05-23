using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
    public interface ISavedPostsRepository
    {
        Saved GetSavedsById(int? SavedPostId);
        void DeleteSavedPost(Saved savedPost);
        void SavePostAtMyPage(Saved savedPost);
        Task<bool> CkeckIfthePostSavedBeforeAsync(int postId, string userId);
        Task SaveChangeAsync ();
        void SaveChange();
    }
}