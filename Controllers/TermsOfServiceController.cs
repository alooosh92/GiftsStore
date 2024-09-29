using GiftsStore.DataModels.PrivacyAndTerm;
using GiftsStore.Models;
using GiftsStore.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GiftsStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TermsOfServiceController : ControllerBase
    {
        public TermsOfServiceController(IRepositoryPrivacyPolicyAndTerm<PrivacyAndTermsAdd, TermsOfService> rep)
        {
            Rep = rep;
        }

        public IRepositoryPrivacyPolicyAndTerm<PrivacyAndTermsAdd, TermsOfService> Rep { get; }

        [HttpPost]
        [Route("CreateTermsOfServic")]
        public async Task<ActionResult<bool>> Add([FromBody] PrivacyAndTermsAdd privacyAndTerms)
        {
            try
            {
                return await Rep.Create(privacyAndTerms);
            }
            catch { throw; }
        }
        [HttpDelete]
        [Route("DeleteTermsOfServic")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {
                return await Rep.Delete(id);
            }
            catch { throw; }
        }
        [HttpGet]
        [Route("TermsOfServicGetAll")]
        public async Task<ActionResult<List<TermsOfService>>> GetAll()
        {
            try
            {
                return await Rep.GetAll();
            }
            catch { throw; }
        }
    }
}
