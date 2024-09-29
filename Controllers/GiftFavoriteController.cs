using GiftsStore.DataModels.FavoriteData;
using GiftsStore.DataModels.GiftFavoriteData;
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
    public class GiftFavoriteController : ControllerBase
    {
        public GiftFavoriteController(
            UserManager<Person> userManager 
            ,IRepositoryAddDeleteGetAll<AddGiftFavorite, ViewGiftFavorite> RepFavorite)
        {
            UserManager = userManager;
            this.RepFavorite = RepFavorite;
        }

        public UserManager<Person> UserManager { get; }
        public IRepositoryAddDeleteGetAll<AddGiftFavorite, ViewGiftFavorite> RepFavorite { get; }

        [HttpPost]
        [Route("CreateGiftFavorite")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ViewGiftFavorite?> CreateFavorite([FromBody] Guid favorite)
        {
            try
            {
                if(!ModelState.IsValid) { return null; }
                var user = UserManager.GetUserId(User);
                if (user == null) { return null; }
                var userr = await UserManager.FindByNameAsync(user!);
                AddGiftFavorite fav = new()
                {
                    Gift = favorite,
                    Person = userr,
                };
                return await RepFavorite.Create(fav);
            }
            catch { throw; }
        }
        [HttpDelete]
        [Route("DeleteGiftFavorite")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<bool> DeleteFavorite(Guid id)
        {
            try
            {
                if (!ModelState.IsValid) { return false; }
                return await RepFavorite.Delete(id);
            }
            catch { throw; }
        }
        [HttpGet]
        [Route("GetAllGiftFavorite")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<List<ViewGiftFavorite>?> GetAllFavorite()
        {
            try
            {
                if (!ModelState.IsValid) { return null; }
                var user = UserManager.GetUserId(User);
                if (user == null) { return null; }
                return await RepFavorite.GetAll(user);
            }
            catch { throw; }
        } 
    }
}
