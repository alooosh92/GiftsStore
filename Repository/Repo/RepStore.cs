using GiftsStore.Data;
using GiftsStore.DataModels.StoreData;
using GiftsStore.Models;
using GiftsStore.Repository.Data;
using GiftsStore.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GiftsStore.Repository.Repo
{
    public class RepStore : IRepositoryWithRate<AddStore, ViewStore>
    {
        public RepStore(ApplicationDbContext Db) => this.Db = Db;       

        public ApplicationDbContext Db { get; }

        public async Task<bool> ActiveOrDelete(dynamic id)
        {
            try
            {
               Store? s = await Search(id);
                if (s == null) { return false; }
                s.Enabled = !s.Enabled;
                Db.Stores.Update(s);
                await Db.SaveChangesAsync();
                return true;
            }
            catch { throw; }
        }

        public async Task<ViewStore> Create(AddStore element)
        {
            try
            {
                Store s = element.ToStore();                
                s.Region = await Db.Regions.FindAsync(element.Region);
                await Db.Stores.AddAsync(s);
                int i = 0;
                Images images = new Images();
                foreach (IFormFile file in element.Files!)
                {
                   StoreImages  stim= await images.AddStoreImage(file,s,i==0);
                    i++;
                    await Db.StoreImages.AddAsync(stim);
                }
                await Db.SaveChangesAsync();
                return s.ToViewStore();
            }
            catch { throw; }
        }

        public async Task<bool> DeleteImage(Guid id)
        {
            try
            {
                Images images = new();
                StoreImages? si = await Db.StoreImages.FindAsync(id);
                if(si == null) { return false; }
                images.DeleteStoreImage(si);
                return true;
            }
            catch { throw; }
        }

        public async Task<ViewStore?> Get(Guid id)
        {
            try
            {
                Store? s = await Search(id);
                if (s == null) { return null; }
                return s.ToViewStore();
            }
            catch { throw; }
        }

        public async Task<List<ViewStore>> GetAll(dynamic? regionId)
        {
            try
            {
                var s = await SearchAllByRegion(regionId);
                var vs = new List<ViewStore>();
                foreach (Store item in s)
                {
                    var stor = item.ToViewStore();
                    var imgs = await Db.StoreImages.Include(a=>a.Store).Where(a=>a.Store!.Id == item.Id && a.Type == "Icon").ToListAsync();
                    foreach (StoreImages item1 in imgs)
                    {
                        stor.ListImage!.Add(item1.ToViewImage());
                    }
                    vs.Add(stor);
                }
                return vs;
            }catch { throw; }
        }

        public async Task<int?> Rate(Person user, Guid id, double rate)
        {
            try
            {
                RateStoreUser? rateUser = await Db.RateStoreUsers.Include(p=>p.Person).Include(s=>s.Store).Where(a=>a.Person == user && a.Store!.Id == id).FirstOrDefaultAsync();
                Store? store = await Search(id);
                if (store == null) { return null; }
                if (rateUser != null)
                {
                    rateUser.Rate = rate;
                    Db.RateStoreUsers.Update(rateUser);
                    await Db.SaveChangesAsync();
                }
                else
                {
                    rateUser = new()
                    {
                        Id = Guid.NewGuid(),
                        Rate = rate,
                        Person = user,
                        Store = await Search(id)
                    };
                    await Db.RateStoreUsers.AddAsync(rateUser);
                    await Db.SaveChangesAsync();
                }
                List<RateStoreUser> list =await Db.RateStoreUsers.Include(s => s.Store).Where(a => a.Store!.Id == id).ToListAsync();
                double r = 0;
                foreach (var item in list) {
                    r += item.Rate;
                }
                store.NumRate = list.Count;  
                store.Rate = r/ store.NumRate;
                Db.Stores.Update(store);
                await Db.SaveChangesAsync();                
                return (int)Math.Round(store.Rate, 0);
            }
            catch { throw; }
        }

        public async Task<ViewStore?> Update(ViewStore element)
        {
            try
            {
                Store? s = await Search(element.Id);
                if (s == null) { return null; }
                s.Description = element.Description;
                s.Late = element.Late;
                s.Long = element.Long;
                s.Mobile = element.Mobile;
                s.Phone = element.Phone;
                s.Name = element.Name;
                Db.Stores.Update(s);
                await Db.SaveChangesAsync();
                return s.ToViewStore();
            }
            catch { throw; }
        }
        
        private async Task<Store?> Search(Guid id)
        {
            try
            {
                Store? s = await Db.Stores.Include(a => a.Region).Where(s => s.Id == id).SingleOrDefaultAsync();
                if (s == null) { return null; }
                return s;
            }
            catch { throw; }
        }
       
        private async Task<List<Store>?> SearchAllByRegion(int id)
        {
            try
            {
                List<Store>? s = new();
                if (Db.Regions.Any(a=>a.Id == id))
                {
                     s = await Db.Stores.Include(a => a.Region).Where(s => s.Region!.Id == id && s.Enabled).OrderByDescending(a=>a.Rate).ThenByDescending(a=>a.NumRate).ToListAsync();
                }
                else
                {
                     s = await Db.Stores.Include(a => a.Region).OrderByDescending(a => a.Rate).ThenByDescending(a => a.NumRate).ToListAsync();
                }
                if (s == null) { return null; }
                return s;
            }
            catch { throw; }
        }
    }
}
