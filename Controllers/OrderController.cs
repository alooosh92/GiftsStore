using GiftsStore.DataModels.OrderData;
using GiftsStore.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GiftsStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public OrderController(IRepositoryOrder<AddOrder, ViewOrder> RepOrder) => this.RepOrder = RepOrder;

        public IRepositoryOrder<AddOrder, ViewOrder> RepOrder { get; }
        [HttpPost]
        [Route("CreateOrder")]
        public async Task<ViewOrder?> Create([FromBody]AddOrder element)
        {
            try
            {
                if(!ModelState.IsValid) {return null;}
                return await RepOrder.Create(element);
            }
            catch { throw; }
        }
        [HttpDelete]
        [Route("DeleteOrder")]
        public async Task<bool> Delete(Guid id)
        {
            try
            {
                if (!ModelState.IsValid) { return false; }
                return await RepOrder.Delete(id);
            }
            catch { throw; }
        }
        [HttpGet]
        [Route("GetOrder")]
        public async Task<ViewOrder?> Get(Guid id)
        {
            try
            {
                if (!ModelState.IsValid) { return null; }
                return await RepOrder.Get(id);
            }
            catch { throw; }
        }
        [HttpGet]
        [Route("GetAllOrder")]
        public async Task<List<ViewOrder>?> GetAll(string id)
        {
            try
            {
                if (!ModelState.IsValid) { return null; }
                return await RepOrder.GetAll(id);
            }
            catch { throw; }
        }
    }
}
