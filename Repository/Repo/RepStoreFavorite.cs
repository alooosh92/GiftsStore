using GiftsStore.Data;
using GiftsStore.DataModels.FavoriteData;
using GiftsStore.DataModels.StoreFavoriteData;
using GiftsStore.Models;
using GiftsStore.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GiftsStore.Repository.Repo
{
    public class RepStoreFavorite : IRepositoryAddDeleteGetAll<AddStoreFavorite, ViewStoreFavorite?>
    {
        public RepStoreFavorite(ApplicationDbContext Db) => this.Db = Db;

        public ApplicationDbContext Db { get; }

        public async Task<ViewStoreFavorite?> Create(AddStoreFavorite element)
        {
            try
            {
                StoreFavorite favorite = element.ToFavorite();
                favorite.Person = await Db.Users.Where(a=>a == element.Person).SingleOrDefaultAsync();
                if(favorite.Person == null) { return null; }
                favorite.Store = await Db.Stores.FindAsync(element.Store);
                var f = Db.StoreFavorite.Include(g => g.Store).Include(p => p.Person).Where(a => a.Store == favorite.Store && a.Person == favorite.Person).ToList();
                if (f.Count > 0)
                {
                    return f.First().ToViewGiftFavorite();
                }
                await Db.StoreFavorite.AddAsync(favorite);
                await Db.SaveChangesAsync();
                return favorite.ToViewGiftFavorite();
            }
            catch { throw; }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                StoreFavorite? favorite = await Search(id);
                if (favorite == null) { return false; }
                Db.StoreFavorite.Remove(favorite);
                await Db.SaveChangesAsync();
                return true;
            }
            catch { throw; }
        }

        public async Task<List<ViewStoreFavorite?>> GetAll(dynamic id)
        {
            try
            {
                List<StoreFavorite> favorites = await SearchAll(id);
                List<ViewStoreFavorite> result = new() { };
                foreach (var item in favorites)
                {
                    result.Add(item.ToViewGiftFavorite());
                }
                return result!;
            }
            catch { throw; }
        }

        private async Task<StoreFavorite?> Search(Guid id)
        {
            try
            {
                StoreFavorite? favorite = await Db.StoreFavorite.Include(a => a.Store!.Region).Include(a => a.Person).Where(a => a.Id == id).SingleOrDefaultAsync();
                if (favorite == null) { return null; }
                return favorite;
            }
            catch { throw; }
        }
        private async Task<List<StoreFavorite>> SearchAll(string id)
        {
            try
            {                
                List<StoreFavorite> favorites = await Db.StoreFavorite.Include(a => a.Store!.Region).Include(a => a.Person).Where(a => a.Person!.PhoneNumber == id).ToListAsync();
                return favorites;
            }
            catch { throw; }
        }
    }
}
