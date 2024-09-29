using GiftsStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GiftsStore.Data
{
    public class ApplicationDbContext : IdentityDbContext<Person>
    {
         public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<DeliveryCompanies> DeliveryCompanies { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<GiftFavorite> GiftFavorite { get; set; }
        public DbSet<StoreFavorite> StoreFavorite { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<GiftImages> GiftImages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<PaymentsDeliveryCompanies> PaymentsDeliveryCompanies { get; set; }
        public DbSet<PaymentsStore> PaymentsStores { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreImages> StoreImages { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<PrivacyPolicy> PrivacyPolicies { get; set; }
        public DbSet<TermsOfService> TermsOfServices { get; set; }
        public DbSet<RateGiftUser> RateGiftUsers { get; set; }
        public DbSet<RateStoreUser> RateStoreUsers { get; set; }
        public DbSet<UserNotices> UserNotices { get; set; }
    }
}
