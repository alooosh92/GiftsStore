using GiftsStore.Data;
using GiftsStore.DataModels.DeliveryCompaniesData;
using GiftsStore.Models;
using GiftsStore.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GiftsStore.Repository.Repo
{
    public class RepDeliveryCompany : IRepositoryDefault<AddDeliveryCompanies, ViewDeliveryCompanies>
    {
        public RepDeliveryCompany(ApplicationDbContext Db) => this.Db = Db;

        public ApplicationDbContext Db { get; }

        public async Task<bool> ActiveOrDelete(dynamic id)
        {
            try
            {
                DeliveryCompanies delivery = await Search(id);
                if (delivery == null) { return false; }
                delivery.Enabled = !delivery.Enabled;
                Db.DeliveryCompanies.Update(delivery);
                await Db.SaveChangesAsync();
                return true;
            }
            catch { throw; }
        }

        public async Task<ViewDeliveryCompanies> Create(AddDeliveryCompanies element)
        {
            try
            {
                DeliveryCompanies delivery = element.ToDeliveryCompanies();
                delivery.Region = await Db.Regions.FindAsync(delivery.Region!.Id);
                await Db.DeliveryCompanies.AddAsync(delivery);
                await Db.SaveChangesAsync();
                return delivery.ToViewDeliveryCompanies();
            }
            catch
            {
                throw;
            }
        }

        public async Task<ViewDeliveryCompanies?> Get(Guid id)
        {
            try
            {
                DeliveryCompanies? delivery = await Search(id);
                if (delivery == null) { return null; }
                return delivery.ToViewDeliveryCompanies();
            }
            catch { throw; }
        }

        public async Task<List<ViewDeliveryCompanies>> GetAll(dynamic? element)
        {
            try
            {
                List<DeliveryCompanies>? deliveries = await SearchAll(element);                
                List<ViewDeliveryCompanies> ViewDelivery = new() { };
                foreach (DeliveryCompanies delivery in deliveries)
                {
                    ViewDelivery.Add(delivery.ToViewDeliveryCompanies());
                }
                return ViewDelivery;
            }
            catch { throw; }
        }

        public async Task<ViewDeliveryCompanies?> Update(ViewDeliveryCompanies element)
        {
            try
            {
                DeliveryCompanies? delivery = await Search(element.Id);
                if (delivery == null) { return null; }
                delivery.Late = element.Late;
                delivery.Long = element.Long;
                delivery.Mobile = element.Mobile;
                delivery.Name = element.Name;
                delivery.Phone = element.Phone;
                delivery.Region = await Db.Regions.FindAsync(element.Region);
                Db.DeliveryCompanies.Update(delivery);
                await Db.SaveChangesAsync();
                return delivery.ToViewDeliveryCompanies();
            }
            catch { throw; }
        }

        private async Task<DeliveryCompanies?> Search(Guid id)
        {
            try
            {
                DeliveryCompanies? delivery = await Db.DeliveryCompanies.Include(a => a.Region).Where(a => a.Id == id).SingleOrDefaultAsync();
                if (delivery == null) { return null; }
                return delivery;
            }
            catch { throw; }
        }
        private async Task<List<DeliveryCompanies>> SearchAll(int id)
        {
            try
            {
                List<DeliveryCompanies> deliveries = await Db.DeliveryCompanies.Include(a => a.Region).Where(a => a.Region!.Id == id).ToListAsync();
                return deliveries;
            }
            catch { throw; }
        }

    }
}
