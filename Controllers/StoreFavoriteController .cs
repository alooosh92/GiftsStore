using GiftsStore.DataModels.FavoriteData;
using GiftsStore.DataModels.GiftFavoriteData;
using GiftsStore.DataModels.StoreFavoriteData;
using GiftsStore.Models;
using GiftsStore.Repository.Interface;
using GiftsStore.Repository.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GiftsStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreFavoriteController : ControllerBase
    {
        public StoreFavoriteController(
            UserManager<Person> userManager
            ,IRepositoryAddDeleteGetAll<AddStoreFavorite, ViewStoreFavorite> RepFavorite)
        {
            UserManager = userManager;
            this.RepFavorite = RepFavorite;
        }

        public UserManager<Person> UserManager { get; }
        public IRepositoryAddDeleteGetAll<AddStoreFavorite, ViewStoreFavorite> RepFavorite { get; }

        [HttpPost]
        [Route("CreateStoreFavorite")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ViewStoreFavorite?> CreateFavorite([FromBody] Guid favorite)
        {
            try
            {
                if(!ModelState.IsValid) { return null; }
                var user =  UserManager.GetUserId(User);
                if (user == null) { return null; }
                var userr = await UserManager.FindByNameAsync(user!);                
                AddStoreFavorite fav = new()
                {
                    Person = userr,
                    Store = favorite,
                };
                return await RepFavorite.Create(fav);
            }
            catch { throw; }
        }
        [HttpDelete]
        [Route("DeleteStoreFavorite")]
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
        [Route("GetAllStoreFavorite")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<List<ViewStoreFavorite>?> GetAllFavorite()
        {
            try
            {
                if (!ModelState.IsValid) { return null; }
                var user =  UserManager.GetUserId(User);
                if (user == null) { return null; }
                return await RepFavorite.GetAll(user);
            }
            catch { throw; }
        }
    }
}
