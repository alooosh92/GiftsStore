using GiftsStore;
using GiftsStore.Data;
using GiftsStore.DataModels.CommentData;
using GiftsStore.DataModels.DeliveryCompaniesData;
using GiftsStore.DataModels.FavoriteData;
using GiftsStore.DataModels.GiftData;
using GiftsStore.DataModels.GiftFavoriteData;
using GiftsStore.DataModels.OfferData;
using GiftsStore.DataModels.OrderData;
using GiftsStore.DataModels.OrderItem;
using GiftsStore.DataModels.PaymentsDeliveryCompaniesData;
using GiftsStore.DataModels.PaymentsStoreData;
using GiftsStore.DataModels.PrivacyAndTerm;
using GiftsStore.DataModels.StoreData;
using GiftsStore.DataModels.StoreFavoriteData;
using GiftsStore.DataModels.UserNotices;
using GiftsStore.Models;
using GiftsStore.Repository.Interface;
using GiftsStore.Repository.Repo;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Abstractions;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;
using System.Drawing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
builder.Services.AddDbContext<ApplicationDbContext>(
  opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddDefaultIdentity<Person>()
 //   .AddRoles<IdentityRole>()
  //  .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRepositoryDefault<AddDeliveryCompanies, ViewDeliveryCompanies>, RepDeliveryCompany>();
builder.Services.AddScoped<IRepositoryAddDeleteGetAll<AddGiftFavorite, ViewGiftFavorite?>, RepGiftFavorite>();
builder.Services.AddScoped<IRepositoryAddDeleteGetAll<AddStoreFavorite, ViewStoreFavorite?>, RepStoreFavorite>();
builder.Services.AddScoped<IRepositoryWithRate<AddGift, ViewGift?>, RepGift>();
builder.Services.AddScoped<IRepositoryOrder<AddOrder, ViewOrder, AddItem, ViewOrderItem>, RepOrder>();
builder.Services.AddScoped<IRepositoryDefault<AddPaymentsDeliveryCompanies, ViewPaymentsDeliveryCompanies>, RepPaymentsDeliveryCompanies>();
builder.Services.AddScoped<IRepositoryDefault<AddPaymentsStore, ViewPaymentsStore>, RepPaymentssStore>();
builder.Services.AddScoped<IRepositoryGetAllActive<Region>, RepRegion>();
builder.Services.AddScoped<IRepositoryWithRate<AddStore, ViewStore>, RepStore>();
builder.Services.AddScoped<IRepositoryPrivacyPolicyAndTerm<PrivacyAndTermsAdd, PrivacyPolicy>, RepPrivacyPolicy>();
builder.Services.AddScoped<IRepositoryPrivacyPolicyAndTerm<PrivacyAndTermsAdd, TermsOfService>, RepTermsOfServies>();
builder.Services.AddScoped<IRepositoryOffer<AddOffer,ViewOffer?>,RepOffer>();
builder.Services.AddScoped<IRepositoryUserNotices<ViewUserNotices>, RepUserNotices>();
builder.Services.AddScoped<IRepositoryComment<AddComment, ViewComment>, RepComment>();

Seed.Setting(builder);
var app = builder.Build();
await Seed.AddRoll(app.Services, new List<string> { "User", "Admin", "Store", "Delivery" });
await Seed.AddAdmin(app.Services, builder.Configuration["AdminPhone"]!);
await Seed.AddRegion(app.Services);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();

