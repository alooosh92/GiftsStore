using GiftsStore.Data;
using GiftsStore.DataModels.PaymentsStoreData;
using GiftsStore.Models;
using GiftsStore.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GiftsStore.Repository.Repo
{
    public class RepPaymentssStore : IRepositoryDefault<AddPaymentsStore, ViewPaymentsStore>
    {
        public RepPaymentssStore(ApplicationDbContext Db) => this.Db = Db;

        public ApplicationDbContext Db { get; }

        public Task<bool> ActiveOrDelete(dynamic id)
        {
            throw new NotImplementedException();
        }

        public async Task<ViewPaymentsStore> Create(AddPaymentsStore element)
        {
            try
            {
                PaymentsStore payment = element.ToPaymentsStore();
                payment.Store = await Db.Stores.FindAsync(element.Store);
                Db.PaymentsStores.Add(payment);
                await Db.SaveChangesAsync();
                return payment.ToViewPaymentsStore();
            }
            catch { throw; }

        }

        public async Task<ViewPaymentsStore?> Get(Guid id)
        {
            try
            {
                PaymentsStore? payment = await Search(id);
                if(payment == null) { return null; }
                return payment.ToViewPaymentsStore();
            }
            catch { throw; }
        }

        public async Task<List<ViewPaymentsStore>> GetAll(dynamic? element)
        {
            try
            {
                List<PaymentsStore> payments = await SearchAll(element);
                List<ViewPaymentsStore> viewPayments = new() { };
                foreach (var pay in payments)
                {
                    viewPayments.Add(pay.ToViewPaymentsStore());
                }
                return viewPayments;
            }
            catch { throw; }
        }

        public async Task<ViewPaymentsStore?> Update(ViewPaymentsStore element)
        {
            try
            {
                PaymentsStore? payment = await Search(element.Id);
                if (payment == null) { return null; }
                payment.Note = element.Note;
                payment.Balance = element.Balance;
                payment.Type = payment.Type;
                Db.PaymentsStores.Update(payment);
                await Db.SaveChangesAsync();
                return payment.ToViewPaymentsStore();
            }
            catch { throw; }
        }
        private async Task<PaymentsStore?> Search(Guid id)
        {
            try
            {
                PaymentsStore? payment = await Db.PaymentsStores.Include(a => a.Store!.Region).Where(a => a.Id == id).SingleOrDefaultAsync();
                if (payment == null) { return null; }
                return payment;
            }
            catch { throw; }
        }
        private async Task<List<PaymentsStore>> SearchAll(Guid id)
        {
            try
            {
                List<PaymentsStore> payments = await Db.PaymentsStores.Include(a => a.Store!.Region).Where(a => a.Store!.Id == id).ToListAsync();
                return payments;
            }
            catch { throw; }
        }
    }
}
