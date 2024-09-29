using GiftsStore.Models;
using GiftsStore.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GiftsStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        public RegionController(IRepositoryGetAllActive<Region> RepRegion) => this.RepRegion = RepRegion;

        public IRepositoryGetAllActive<Region> RepRegion { get; }
        [HttpPut]
        [Route("ActiveRegion")]
        public async Task<bool> ActiveOrDelete(int id)
        {
            try
            {
                if (!ModelState.IsValid) { return false; }
                return await RepRegion.ActiveOrDelete(id);
            }
            catch { throw; }
        }
        [HttpGet]
        [Route("GetAllRegion")]
        public async Task<List<Region>?> GetAll()
        {
            try
            {
                if (!ModelState.IsValid) { return null; }
                return await RepRegion.GetAll(null);
            }
            catch { throw; }
        }
    }
}