using GiftsStore.Data;
using GiftsStore.DataModels.GiftData;
using GiftsStore.DataModels.StoreData;
using GiftsStore.Models;
using GiftsStore.Repository.Data;
using GiftsStore.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace GiftsStore.Repository.Repo
{
    public class RepGift : IRepositoryWithRate<AddGift, ViewGift>
    {
        public RepGift(ApplicationDbContext Db) {
            this.Db = Db;
        }

        public ApplicationDbContext Db { get; }

        public async Task<bool> ActiveOrDelete(dynamic id)
        {
            try
            {
                Gift? gift = await Search(id);
                if (gift == null) { return false; }
                gift.Enabled = !gift.Enabled;
                Db.Gifts.Update(gift);
                await Db.SaveChangesAsync();
                return true;
            }
            catch { throw; }
        }

        public async Task<ViewGift> Create(AddGift element)
        {
            try
            {
                Gift gift = element.ToGift();
                gift.Store = await Db.Stores.FindAsync(gift.Store);
                await Db.Gifts.AddAsync(gift);
                await Db.SaveChangesAsync();
                return gift.toViewGift();
            }
            catch { throw; }
        }

        public async Task<bool> DeleteImage(Guid id)
        {
            Images images = new Images();
            GiftImages? im = await Db.GiftImages.FindAsync(id);
            if (im == null) { return false; }
            return images.DeleteGiftImage(im);
        }

        public async Task<ViewGift?> Get(Guid id)
        {
            try
            {
                Gift? gift = await Search(id);
                if (gift == null) { return null; }
                return gift.toViewGift();
            }
            catch { throw; }
        }

        public async Task<List<ViewGift>> GetAll(dynamic? element)
        {
            try
            {
                List<Gift> gifts = await SearchAll(element);
                List<ViewGift> vGigt = new() { };
                foreach (Gift gift in gifts)
                {
                    vGigt.Add(gift.toViewGift());
                }
                return vGigt;
            }
            catch { throw; }
        }

        public async Task<int?> Rate(Person user,Guid id, double rate)
        {
            try
            {
                RateGiftUser? rateUser = await Db.RateGiftUsers.Include(p => p.Person).Include(s => s.Gift).Where(a => a.Person == user && a.Gift!.Id == id).FirstOrDefaultAsync();
                Gift? gift = await Search(id);
                if (gift == null) { return null; }
                if (rateUser != null)
                {
                    rateUser.Rate = rate;
                    Db.RateGiftUsers.Update(rateUser);
                    await Db.SaveChangesAsync();
                }
                else
                {
                    rateUser = new()
                    {
                        Id = Guid.NewGuid(),
                        Rate = rate,
                        Person = user,
                        Gift = await Search(id)
                    };
                    await Db.RateGiftUsers.AddAsync(rateUser);
                    await Db.SaveChangesAsync();
                }
                List<RateStoreUser> list = await Db.RateStoreUsers.Include(s => s.Store).Where(a => a.Store!.Id == id).ToListAsync();
                double r = 0;
                foreach (var item in list)
                {
                    r += item.Rate;
                }
                gift.NumRate = list.Count;
                gift.Rate = r / gift.NumRate;
                Db.Gifts.Update(gift);
                await Db.SaveChangesAsync();
                return (int)Math.Round(gift.Rate, 0);
            }
            catch { throw; }
        }

        public async Task<ViewGift?> Update(ViewGift element)
        {
            try
            {
                Gift? gift = await Search(element.Id);
                if (gift == null) { return null; }
                gift.Price = element.Price;
                gift.Description = element.Description;
                gift.Name = element.Name;
                Db.Gifts.Update(gift);
                await Db.SaveChangesAsync();
                return gift.toViewGift();
            }
            catch { throw; }
        }

        private async Task<Gift?> Search(Guid id)
        {
            try
            {
                Gift? g = await Db.Gifts.Include(a => a.Store!.Region).Where(s => s.Id == id).SingleOrDefaultAsync();
                if (g == null) { return null; }
                return g;
            }
            catch { throw; }
        }

        private async Task<List<Gift>> SearchAll(Guid id)
        {
            try
            {
                List<Gift> g = await Db.Gifts.Include(a => a.Store!.Region).Where(s => s.Store!.Id == id).OrderByDescending(a => a.Rate).ThenByDescending(a => a.NumRate).ToListAsync();                
                return g;
            }
            catch { throw; }
        }
    }
}
