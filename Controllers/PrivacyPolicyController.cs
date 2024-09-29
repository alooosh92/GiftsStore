using GiftsStore.DataModels.PrivacyAndTerm;
using GiftsStore.Models;
using GiftsStore.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GiftsStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrivacyPolicyController : ControllerBase
    {
        public PrivacyPolicyController(IRepositoryPrivacyPolicyAndTerm<PrivacyAndTermsAdd, PrivacyPolicy> rep)
        {
            Rep = rep;
        }

        public IRepositoryPrivacyPolicyAndTerm<PrivacyAndTermsAdd, PrivacyPolicy> Rep { get; }

        [HttpPost]
        [Route("CreatePrivacyPolicy")]
        public async Task<ActionResult<bool>> Add([FromBody] PrivacyAndTermsAdd privacyAndTerms)
        {
            try
            {
               return await Rep.Create(privacyAndTerms);
            }
            catch { throw; }
        }
        [HttpDelete]
        [Route("DeletePrivacyPolicy")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {
                return await Rep.Delete(id);
            }
            catch { throw; }
        }
        [HttpGet]
        [Route("GetAllPrivacyPolicy")]
        public async Task<ActionResult<List<PrivacyPolicy>>> GetAll()
        {
            try
            {
                return await Rep.GetAll();
            }
            catch { throw; }
        }
    }
}
