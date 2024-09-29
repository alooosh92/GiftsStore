using GiftsStore.DataModels.UserNotices;
using GiftsStore.Models;
using GiftsStore.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GiftsStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserNoticesController : ControllerBase
    {
        public UserNoticesController(IRepositoryUserNotices<ViewUserNotices> repositoryUser, UserManager<Person> userManager)
        {
            RepositoryUser = repositoryUser;
            UserManager = userManager;
        }

        public IRepositoryUserNotices<ViewUserNotices> RepositoryUser { get; }
        public UserManager<Person> UserManager { get; }

        [HttpGet]
        [Route("GetNumNotices")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<int>?> GetNumNotices()
        {
            try
            {
                if (!ModelState.IsValid) { return null; }
                var user = UserManager.GetUserId(User);
                if (user == null) { return null; }
                var userr = await UserManager.FindByNameAsync(user!);
                if (userr == null) { return null; }
                return await RepositoryUser.GetNumUnRead(userr);
            }
            catch { throw; }
        }

        [HttpGet]
        [Route("GetNotices")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<List<ViewUserNotices>>?> GetNotices()
        {
            try
            {
                if (!ModelState.IsValid) { return null; }
                var user = UserManager.GetUserId(User);
                if (user == null) { return null; }
                var userr = await UserManager.FindByNameAsync(user!);
                if (userr == null) { return null; }
                return await RepositoryUser.Getfirst50(userr);
            }
            catch { throw; }
        }
    }
}
