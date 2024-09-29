using GiftsStore.DataModels.StoreData;
using GiftsStore.Models;
using GiftsStore.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace GiftsStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        public StoreController(IRepositoryWithRate<AddStore, ViewStore> repStore,UserManager<Person> userManager)
        {
            RepStore = repStore;
            UserManager = userManager;
        }

        public IRepositoryWithRate<AddStore, ViewStore> RepStore { get; }
        public UserManager<Person> UserManager { get; }

        [HttpPost]
        [Route("AddStore")]
        public async Task<ActionResult<ViewStore>?> AddStore([FromForm] AddStore store)
        {
            try
            {
                if(!ModelState.IsValid) { return null; }
                 return await RepStore.Create(store);
            }
            catch { throw; }
        }
        [HttpPut]
        [Route("ActiveStore")]
        public async Task<ActionResult<bool>> ActiveStore(Guid id)
        {
            try
            {
                if (!ModelState.IsValid) { return false; }
                return await RepStore.ActiveOrDelete(id);
            }
            catch { throw; }
        }
        [HttpDelete]
        [Route("DeleteStoreImage")]
        public async Task<ActionResult<bool>> DeleteStoreImage(Guid id)
        {
            try
            {
                if (!ModelState.IsValid) { return false; }
                return await RepStore.DeleteImage(id);
            }
            catch { throw; }
        }
        [HttpGet]
        [Route("GetStore")]
        public async Task<ActionResult<ViewStore?>?> GetStore(Guid id)
        {
            try
            {
                if (!ModelState.IsValid) { return null; }
                return await RepStore.Get(id);
            }
            catch { throw; }

        }
        [HttpGet]
        [Route("GetAllStore")]
        public async Task<ActionResult<List<ViewStore>>?> GetAllStore(int id)
        {
            try
            {
                if(!ModelState.IsValid) { return null; }
                return await RepStore.GetAll(id);
            }
            catch { throw; }
        }
        [HttpPut]
        [Route("UpdateStore")]
        public async Task<ActionResult<ViewStore?>?> UpdateStore([FromBody] ViewStore store)
        {
            try
            {
                if (!ModelState.IsValid) { return null; }
                return await RepStore.Update(store);
            }
            catch { throw; }
        }

        [HttpGet]
        [Route("RateStore")]
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
                return await RepStore.Rate(userr, id, rate);
            }
            catch { throw; }
        }
    }
}
