using GiftsStore.Data;
using GiftsStore.DataModels.ImageData;
using GiftsStore.DataModels.OfferData;
using GiftsStore.DataModels.OrderData;
using GiftsStore.Models;
using GiftsStore.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GiftsStore.Repository.Repo
{
    public class RepOffer : IRepositoryOffer<AddOffer, ViewOffer?>
    {
        public RepOffer(ApplicationDbContext DB)
        {
            this.DB = DB;
        }

        public ApplicationDbContext DB { get; }

        public async Task<ViewOffer?> Create(AddOffer element)
        {
            try
            {
                if (element.StartDate > element.EndDate) { return null; }
                Offers offer = element.ToOffer();
                bool test = await DB.Offers.Include(a => a.Gift).AnyAsync(a => a.Gift!.Id == offer.Gift!.Id && a.EndDate > DateTime.Now);
                if (test) { return null; }
                Gift? gift = await DB.Gifts.Include(a=>a.Store!.Region).SingleOrDefaultAsync(a => a.Id == element.Gift);
                if (gift == null) { return null; }
                offer.Gift = gift;
                await DB.Offers.AddAsync(offer);
                await DB.SaveChangesAsync();
                ViewOffer viewOffer = offer.ToViewOffer();
                List<GiftImages> giftImage = await DB.GiftImages.Include(a => a.Gift).Where(a => a.Gift!.Id == gift.Id).ToListAsync();
                foreach (var item in giftImage)
                {
                    viewOffer.GiftImages!.Add(new ViewImage { Type = item.Type,URL = item.URL});
                }
                return viewOffer;
            }
            catch { throw; }
        }

        public async Task<ViewOffer?> Get(Guid id)
        {
            try
            {
                Offers? offer = await DB.Offers.Include(a=>a.Gift!.Store!.Region).SingleOrDefaultAsync(a=>a.Id == id);
                if (offer == null) { return null; };
                return offer.ToViewOffer();
            }
            catch { throw; }
        }

        public async Task<List<ViewOffer?>> GetAll(Guid element)
        {
            try
            {
                List<ViewOffer?> vOffers = [];
                List<Offers> offers = await DB.Offers.Include(a => a.Gift!.Store!.Region).Where(a => a.Gift!.Store!.Id == element && a.EndDate > DateTime.Now).ToListAsync();
                foreach (var item in offers)
                {
                    ViewOffer viewOffer = item.ToViewOffer();
                    List<GiftImages> giftImage = await DB.GiftImages.Include(a => a.Gift).Where(a => a.Gift!.Id == viewOffer.GiftId).ToListAsync();
                    foreach (var img in giftImage)
                    {
                        viewOffer.GiftImages!.Add(new ViewImage { Type = img.Type, URL = img.URL });
                    }
                    vOffers.Add(viewOffer);
                }
                return vOffers;
            }
            catch { throw; }
        }

        public async Task<bool> Update(Guid element)
        {
            try
            {
                Offers? offer = await DB.Offers.SingleOrDefaultAsync(a=>a.Id == element);
                if (offer == null) { return false; };
                offer.EndDate = DateTime.Now;
                DB.Offers.Update(offer);
                await DB.SaveChangesAsync();
                return true;
            }
            catch { throw; }
        }
    }
}
