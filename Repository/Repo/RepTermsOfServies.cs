using GiftsStore.Data;
using GiftsStore.DataModels.PrivacyAndTerm;
using GiftsStore.Models;
using GiftsStore.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GiftsStore.Repository.Repo
{
    public class RepTermsOfServies : IRepositoryPrivacyPolicyAndTerm<PrivacyAndTermsAdd, TermsOfService>
    {
        public RepTermsOfServies(ApplicationDbContext Db)
        {
            this.Db = Db;
        }

        public ApplicationDbContext Db { get; }

        public async Task<bool> Create(PrivacyAndTermsAdd element)
        {
            try
            {
                await Db.TermsOfServices.AddAsync(element.ToTermsOfService());
                await Db.SaveChangesAsync();
                return true;
            }
            catch { throw;}
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                TermsOfService? tos = await Db.TermsOfServices.FindAsync(id);
                if (tos == null) { return false; }
                Db.Remove(tos);
                await Db.SaveChangesAsync();
                return true;
            }
            catch { throw; }
        }

        public async Task<List<TermsOfService>> GetAll()
        {
            try
            {
                return await Db.TermsOfServices.ToListAsync();
            }
            catch { throw; }
        }
    }
}
