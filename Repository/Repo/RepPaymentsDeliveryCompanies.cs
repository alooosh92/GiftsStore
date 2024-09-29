using GiftsStore.Data;
using GiftsStore.DataModels.PaymentsDeliveryCompaniesData;
using GiftsStore.Models;
using GiftsStore.Repository.Interface;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;

namespace GiftsStore.Repository.Repo
{
    public class RepPaymentsDeliveryCompanies : IRepositoryDefault<AddPaymentsDeliveryCompanies, ViewPaymentsDeliveryCompanies>
    {
        public RepPaymentsDeliveryCompanies(ApplicationDbContext Db) => this.Db = Db;

        public ApplicationDbContext Db { get; }

        public Task<bool> ActiveOrDelete(dynamic id)
        {
            throw new NotImplementedException();
        }

        public async Task<ViewPaymentsDeliveryCompanies> Create(AddPaymentsDeliveryCompanies element)
        {
            try
            {
                PaymentsDeliveryCompanies payment = element.ToPaymentsDeliveryCompanies();
                payment.DeliveryCompanies = await Db.DeliveryCompanies.FindAsync(element.DeliveryCompanies);
                await Db.PaymentsDeliveryCompanies.AddAsync(payment);
                await Db.SaveChangesAsync();
                return payment.ToViewPaymentsDeliveryCompanie();
            }
            catch { throw; }
        }

        public async Task<ViewPaymentsDeliveryCompanies?> Get(Guid id)
        {
            try
            {
                PaymentsDeliveryCompanies? payment = await Search(id);
                if(payment == null) { return null; }
                return payment.ToViewPaymentsDeliveryCompanie();
            }
            catch { throw; }
        }

        public async Task<List<ViewPaymentsDeliveryCompanies>> GetAll(dynamic? element)
        {
            try
            {
                List<PaymentsDeliveryCompanies> payments = await SearchAll(element);
                List<ViewPaymentsDeliveryCompanies> viewPayments = new() { };
                foreach (var pay in payments)
                {
                    viewPayments.Add(pay.ToViewPaymentsDeliveryCompanie());
                }
                return viewPayments;
            }
            catch { throw; }
        }

        public async Task<ViewPaymentsDeliveryCompanies?> Update(ViewPaymentsDeliveryCompanies element)
        {
            try
            {
                PaymentsDeliveryCompanies? payment = await Search(element.Id);
                if (payment == null) { return null; }
                payment.Note = payment.Note;
                payment.Balance = payment.Balance;
                payment.Type = payment.Type;
                Db.PaymentsDeliveryCompanies.Update(payment);
                await Db.SaveChangesAsync();
                return payment.ToViewPaymentsDeliveryCompanie();
            }
            catch { throw; }
        }
        private async Task<PaymentsDeliveryCompanies?> Search(Guid id)
        {
            try
            {
                PaymentsDeliveryCompanies? payment = await Db.PaymentsDeliveryCompanies.Include(a=>a.DeliveryCompanies!.Region).Where(a=>a.Id == id).SingleOrDefaultAsync();
                if(payment == null) { return  null; }
                return payment;
            }
            catch { throw; }
        }
        private async Task<List<PaymentsDeliveryCompanies>> SearchAll(Guid id)
        {
            try
            {
                List<PaymentsDeliveryCompanies> payment = await Db.PaymentsDeliveryCompanies.Include(a => a.DeliveryCompanies!.Region).Where(a => a.DeliveryCompanies!.Id == id).ToListAsync();                
                return payment;
            }
            catch { throw; }
        }
    }
}