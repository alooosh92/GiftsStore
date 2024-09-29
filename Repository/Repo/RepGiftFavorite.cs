using GiftsStore.Data;
using GiftsStore.DataModels.FavoriteData;
using GiftsStore.DataModels.GiftFavoriteData;
using GiftsStore.Models;
using GiftsStore.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GiftsStore.Repository.Repo
{
    public class RepGiftFavorite : IRepositoryAddDeleteGetAll<AddGiftFavorite, ViewGiftFavorite?>
    {
        public RepGiftFavorite(ApplicationDbContext Db) => this.Db = Db;

        public ApplicationDbContext Db { get; }

        public async Task<ViewGiftFavorite?> Create(AddGiftFavorite element)
        {
            try
            {
                GiftFavorite favorite = element.ToFavorite();
                favorite.Person = await Db.Users.Where(a => a == element.Person).SingleOrDefaultAsync();
                if (favorite.Person == null) { return null; }
                favorite.Gift = await Db.Gifts.FindAsync(element.Gift);
                var f = Db.GiftFavorite.Include(g => g.Gift).Include(p => p.Person).Where(a => a.Gift == favorite.Gift && a.Person == favorite.Person).ToList();
                if (f.Count>0)
                {
                    return f.First().ToViewGiftFavorite();
                }
                await Db.GiftFavorite.AddAsync(favorite);
                await Db.SaveChangesAsync();
                return favorite.ToViewGiftFavorite();
            }
            catch { throw; }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                GiftFavorite? favorite = await Search(id);
                if (favorite == null) { return false; }
                Db.GiftFavorite.Remove(favorite);
                await Db.SaveChangesAsync();
                return true;
            }
            catch { throw; }
        }

        public async Task<List<ViewGiftFavorite?>> GetAll(dynamic id)
        {
            try
            {
                List<GiftFavorite> favorites = await SearchAll(id);
                List<ViewGiftFavorite> result = new() { };
                foreach (var item in favorites)
                {
                    result.Add(item.ToViewGiftFavorite());
                }
                return result!;
            }
            catch { throw; }
        }

        private async Task<GiftFavorite?> Search(Guid id) 
        {
            try
            {
                GiftFavorite? favorite = await Db.GiftFavorite.Include(a=>a.Gift!.Store!.Region).Include(a=>a.Person).Where(a=>a.Id == id).SingleOrDefaultAsync();
                if (favorite == null) { return null; }
                return favorite;
            }
            catch { throw; }
        }
        private async Task<List<GiftFavorite>> SearchAll(string id) 
        {
            try
            {
                List<GiftFavorite> favorites = await Db.GiftFavorite.Include(a => a.Gift!.Store!.Region).Include(a => a.Person).Where(a => a.Person!.PhoneNumber == id).ToListAsync();
                return favorites;
            }
            catch { throw; }
        }
    }
}
