using GiftsStore.Data;
using GiftsStore.DataModels.PrivacyAndTerm;
using GiftsStore.Models;
using GiftsStore.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Xml.Linq;

namespace GiftsStore.Repository.Repo
{
    public class RepPrivacyPolicy : IRepositoryPrivacyPolicyAndTerm <PrivacyAndTermsAdd, PrivacyPolicy>
    {
        public RepPrivacyPolicy(ApplicationDbContext Db)
        {
            this.Db = Db;
        }

        public ApplicationDbContext Db { get; }

        public async Task<bool> Create(PrivacyAndTermsAdd element)
        {
            try
            {
                await Db.PrivacyPolicies.AddAsync(element.ToPrivacyPolicy());
                await Db.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                PrivacyPolicy? pp = await Db.PrivacyPolicies.FindAsync(id);
                if (pp == null) { return false; }
                Db.PrivacyPolicies.Remove(pp);
                await Db.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<PrivacyPolicy>> GetAll()
        {
            try
            {
               return await Db.PrivacyPolicies.ToListAsync();                
            }
            catch
            {
                throw;
            }
        }
    }
}
