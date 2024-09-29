using GiftsStore.DataModels.PaymentsDeliveryCompaniesData;
using GiftsStore.DataModels.PaymentsStoreData;
using GiftsStore.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GiftsStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentStoreController : ControllerBase
    {
       public PaymentStoreController(IRepositoryDefault<AddPaymentsStore, ViewPaymentsStore> RepPaySt) => this.RepPaySt = RepPaySt;
       public IRepositoryDefault<AddPaymentsStore, ViewPaymentsStore> RepPaySt { get; }
        [HttpPut]
        [Route("ActivePaySt")]
        public async Task<bool> ActiveOrDelete(Guid id)
        {
            try
            {
                if (!ModelState.IsValid) { return false; }
                return await RepPaySt.ActiveOrDelete(id);
            }
            catch { throw; }
        }
        [HttpPost]
        [Route("CreatePaySt")]
        public async Task<ViewPaymentsStore?> Create([FromBody] AddPaymentsStore element)
        {
            try
            {
                if (!ModelState.IsValid) { return null; }
                return await RepPaySt.Create(element);
            }
            catch { throw; }
        }
        [HttpGet]
        [Route("GetPaySt")]
        public async Task<ViewPaymentsStore?> Get(Guid id)
        {
            try
            {
                if (!ModelState.IsValid) { return null; }
                return await RepPaySt.Get(id);
            }
            catch { throw; }
        }
        [HttpGet]
        [Route("GetAllSt")]
        public async Task<List<ViewPaymentsStore>?> GetAll(Guid? element)
        {
            try
            {
                if (!ModelState.IsValid) { return null; }
                return await RepPaySt.GetAll(element);
            }
            catch { throw; }
        }
        [HttpPut]
        [Route("UpdatePaySt")]
        public async Task<ViewPaymentsStore?> Update([FromBody] ViewPaymentsStore element)

        {
            try
            {
                if (!ModelState.IsValid) { return null; }
                return await RepPaySt.Update(element);
            }
            catch { throw; }
        }
    }
}
