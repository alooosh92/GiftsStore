using GiftsStore.DataModels.OfferData;
using GiftsStore.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GiftsStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IRepositoryOffer<AddOffer, ViewOffer?> repositoryOffer;

        public OfferController(IRepositoryOffer<AddOffer,ViewOffer?> repositoryOffer)
        {
            this.repositoryOffer = repositoryOffer;
        }


        [HttpPost]
        [Route("AddOffer")]
        public async Task<ViewOffer?>? Create([FromBody]AddOffer element)
        {
            try
            {
                return await repositoryOffer.Create(element);
            }
            catch { throw; }
        }
        [HttpGet]
        [Route("GetStoreOffer")]
        public async Task<List<ViewOffer?>> GetAll(Guid element)
        {
            try
            {
                return await repositoryOffer.GetAll(element);
            }
            
            catch { throw; }
        }
        [HttpGet]
        [Route("GetOffer")]
        public async Task<ViewOffer?>? Get(Guid element)
        {
            try
            {
                return await repositoryOffer.Get(element);
            }
            catch { throw; }
        }
        [HttpDelete]
        [Route("UnActiveOffer")]
        public async Task<bool> Delete(Guid element)
        {
            try
            {
                return await repositoryOffer.Update(element);
            }
            catch { throw; }
        }
    }
}
