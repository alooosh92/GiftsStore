using GiftsStore.DataModels.OrderData;
using GiftsStore.DataModels.OrderItem;
using GiftsStore.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace GiftsStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public OrderController(IRepositoryOrder<AddOrder, ViewOrder,AddItem,ViewOrderItem> RepOrder) => this.RepOrder = RepOrder;

        public IRepositoryOrder<AddOrder, ViewOrder, AddItem, ViewOrderItem> RepOrder { get; }
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
        [HttpPut]
        [Route("Verification")]
        public async Task<bool> Verification(Guid id)
        {
            try
            {
                return await RepOrder.Verification(id);
            }
            catch { throw; }
        }
        [HttpPut]
        [Route("AppRoval")]
        public async Task<bool> AppRoval(Guid id)
        {
            try
            {
                return await RepOrder.AppRoval(id);
            }
            catch { throw; }
        }       
        [HttpPut]
        [Route("Ready")]
        public async Task<bool> Ready(Guid id)
        {
            try
            {
                return await RepOrder.Ready(id);
            }
            catch { throw; }
        }
        [HttpPut]
        [Route("WaitingForDelivery")]
        public async Task<bool> WaitingForDelivery(Guid id)
        {
            try
            {
                return await RepOrder.WaitingForDelivery(id);
            }
            catch { throw; }
        }
        [HttpPut]
        [Route("Delivered")]
        public async Task<bool> Delivered(Guid id)
        {
            try
            {
                return await RepOrder.Delivered(id);
            }
            catch { throw; }
        }
        [HttpPost]
        [Route("AddItemToOrder")]
        public async Task<bool> AddItemToOrder([FromBody]AddItem element)
        {
            try
            {
                if (!ModelState.IsValid) { return false; }
                return await RepOrder.AddItemToOrder(element);
            }
            catch { throw; }
        }
        [HttpDelete]
        [Route("DeleteItemFromOrder")]
        public async Task<bool> DeleteItemFromOrder(Guid id)
        {
            try
            {
                if (!ModelState.IsValid) { return false; }
                return await RepOrder.DeleteItemFromOrder(id);
            }
            catch { throw; }
        }
        [HttpPut]
        [Route("UpdateItemInOrder")]
        public async Task<bool> UpdateItemInOrder(Guid id, int num)
        {
            try
            {
                if (!ModelState.IsValid) { return false; }
                return await RepOrder.UpdateItemInOrder(id, num);
            }
            catch { throw; }
        }
    }
}
