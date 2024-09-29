using GiftsStore.DataModels.FavoriteData;
using GiftsStore.DataModels.GiftData;
using GiftsStore.Models;
using GiftsStore.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GiftsStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftController : ControllerBase
    {
        public GiftController(IRepositoryWithRate<AddGift, ViewGift> RepGift,UserManager<Person> userManager)
        {
            this.RepGift = RepGift;
            UserManager = userManager;
        }

        public IRepositoryWithRate<AddGift, ViewGift> RepGift { get; }
        public UserManager<Person> UserManager { get; }

        [HttpPut]
        [Route("ActiveGift")]
        public async Task<bool> ActiveGift(Guid id)
        {
            try
            {
                if (!ModelState.IsValid) { return false; }
                return await RepGift.ActiveOrDelete(id);
            }
            catch { throw; }
        }
        [HttpPost]
        [Route("CreateGift")]
        public async Task<ViewGift?> CreateGift([FromForm] AddGift gift)
        {
            try
            {
                if (!ModelState.IsValid) { return null; }
                return await RepGift.Create(gift);
            }
            catch { throw; }
        }
        [HttpDelete]
        [Route("DeleteGiftImage")]
        public async Task<bool> DeleteImage(Guid id)
        {
            try
            {
                if (!ModelState.IsValid) { return  false; }
                return await RepGift.DeleteImage(id);
            }
            catch { throw; }
        }
        [HttpGet]
        [Route("GetGift")]
        public async Task<ViewGift?> GetGift(Guid id)
        {
            try
            {
                if (!ModelState.IsValid) { return null; }
                return await RepGift.Get(id);
            }
            catch { throw; }
        }
        [HttpGet]
        [Route("GetAllGift")]
        public async Task<List<ViewGift>?> GetAllFavorite(string id)
        {
            try
            {
                if(!ModelState.IsValid) { return null; }
                return await RepGift.GetAll(id);
            }
            catch { throw; }
        }
        [HttpGet]
        [Route("RateGift")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<int?> Rate(Guid id, double rate)
        {
            try
            {
                if (!ModelState.IsValid) { return null; }
                var user = UserManager.GetUserId(User);
                if (user == null) { return null; }
                var userr = await UserManager.FindByNameAsync(user!);
                if (userr == null) { return null; }
                return await RepGift.Rate(userr ,id ,rate);
            }
            catch { throw; }
        }
        [HttpPut]
        [Route("UpdateGift")]
        public async Task<ViewGift?> Update([FromBody]ViewGift element)
        {
            try
            {
                if (ModelState.IsValid) { return null; }
                return await RepGift.Update(element);
            }
            catch { throw; }
        }
    }
}
