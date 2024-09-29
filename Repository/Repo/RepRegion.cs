using GiftsStore.Data;
using GiftsStore.Models;
using GiftsStore.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GiftsStore.Repository.Repo
{
    public class RepRegion : IRepositoryGetAllActive<Region>
    {
        public RepRegion(ApplicationDbContext Db) => this.Db = Db;

        public ApplicationDbContext Db { get; }

        public async Task<bool> ActiveOrDelete(dynamic id)
        {
            try
            {
                Region? r = await Db.Regions.FindAsync(id);
                if (r == null) { return false; }
                r.Enabled = !r.Enabled;
                Db.Regions.Update(r);
                await Db.SaveChangesAsync();
                return true;
            }
            catch { throw; }
        }

        public async Task<List<Region>> GetAll(dynamic? element)
        {
            try
            {
                return await Db.Regions.ToListAsync();
            }
            catch { throw; }
        }
    }
}
