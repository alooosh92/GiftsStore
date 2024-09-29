using GiftsStore.DataModels.DeliveryCompaniesData;
using GiftsStore.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GiftsStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryCompanyController : ControllerBase
    {
        public DeliveryCompanyController(IRepositoryDefault<AddDeliveryCompanies, ViewDeliveryCompanies> RepDeliveryCompany) => this.RepDeliveryCompany = RepDeliveryCompany;

        public IRepositoryDefault<AddDeliveryCompanies, ViewDeliveryCompanies> RepDeliveryCompany { get; }
        [HttpPut]
        [Route("ActiveCompany")]
        public async Task<bool> ActiveCompany(Guid id)
        {
            try
            {
                if (!ModelState.IsValid) { return false; }
                return await RepDeliveryCompany.ActiveOrDelete(id);
            }
            catch { throw; }
        }
        [HttpPost]
        [Route("AddDeliveryCompany")]
        public async Task<ViewDeliveryCompanies?> AddDeliveryCompany([FromBody] AddDeliveryCompanies company)
        {
            try
            {
                if (!ModelState.IsValid) { return null; }
                return await RepDeliveryCompany.Create(company);
            }
            catch { throw; }
        }
        [HttpGet]
        [Route("GetDeliveryCompany")]
        public async Task<ViewDeliveryCompanies?> GetDeliveryCompany(Guid id)
        {
            try
            {
                if (!ModelState.IsValid) { return null; }
                return await RepDeliveryCompany.Get(id);
            }catch { throw; }
        }
        [HttpGet]
        [Route("GetAllDeliveryCompany")]
        public async Task<List<ViewDeliveryCompanies>?> GetAllDeliveryCompany(int region)
        {
            try
            {
                if (!ModelState.IsValid) { return null; }
                return await RepDeliveryCompany.GetAll(region);
            }
            catch { throw; }
        }
        [HttpPut]
        [Route("UpdateDeliveryCompany")]
        public async Task<ViewDeliveryCompanies?> UpdateDeliveryCompany([FromBody] ViewDeliveryCompanies company)
        {
            try
            {
                if(!ModelState.IsValid) { return null; }
                return await RepDeliveryCompany.Update(company);
            }
            catch { throw; }
        }
    }
}
