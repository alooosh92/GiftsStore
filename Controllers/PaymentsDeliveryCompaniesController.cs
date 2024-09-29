using GiftsStore.DataModels.PaymentsDeliveryCompaniesData;
using GiftsStore.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GiftsStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsDeliveryCompaniesController : ControllerBase
    {
        public PaymentsDeliveryCompaniesController(IRepositoryDefault<AddPaymentsDeliveryCompanies, ViewPaymentsDeliveryCompanies> RepDC) => this.RepDC = RepDC;

        public IRepositoryDefault<AddPaymentsDeliveryCompanies, ViewPaymentsDeliveryCompanies> RepDC { get; }
        [HttpPut]
        [Route("ActivePayDelivCm")]
        public async Task<bool> ActiveOrDelete(Guid id)
        {
            try
            {
                if (!ModelState.IsValid) { return false; }
                return await RepDC.ActiveOrDelete(id);
            }
            catch { throw; }
        }
        [HttpPost]
        [Route("CreatePayDelivCm")]
        public async Task<ViewPaymentsDeliveryCompanies?> Create([FromBody]AddPaymentsDeliveryCompanies element)
        {
            try
            {
                if (!ModelState.IsValid) { return null; }
                return await RepDC.Create(element);
            }
            catch { throw; }
        }
        [HttpGet]
        [Route("GetPayDelivCm")]
        public async Task<ViewPaymentsDeliveryCompanies?> Get(Guid id)
        {
            try
            {
                if (!ModelState.IsValid) { return null; }
                return await RepDC.Get(id);
            }
            catch { throw; }
        }
        [HttpGet]
        [Route("GetAllPayDelivCm")]
        public async Task<List<ViewPaymentsDeliveryCompanies>?> GetAll(Guid? element)
        {
            try
            {
                if (!ModelState.IsValid) { return null; }
                return await RepDC.GetAll(element);
            }
            catch { throw; }
        }
        [HttpPut]
        [Route("UpdatePayDelivCm")]
        public async Task<ViewPaymentsDeliveryCompanies?> Update([FromBody]ViewPaymentsDeliveryCompanies element)

        {
            try
            {
                if (!ModelState.IsValid) { return null; }
                return await RepDC.Update(element);
            }
            catch { throw; }
        }
    }
}
