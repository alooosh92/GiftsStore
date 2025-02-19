using GiftsStore.DataModels.CommentData;
using GiftsStore.Models;
using GiftsStore.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GiftsStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        public CommentController(IRepositoryComment<AddComment,ViewComment> RepComment,UserManager<Person> UserM)
        {
            this.RepComment = RepComment;
            this.UserM = UserM;
        }

        public IRepositoryComment<AddComment, ViewComment> RepComment { get; }
        public UserManager<Person> UserM { get; }

        [HttpPost]
        [Route("AddComment")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ViewComment?> AddComment([FromBody] AddComment comment)
        {
            try 
            {
                var per = UserM.GetUserId(User);
                if(per == null) { return null; }
                Person? person = await UserM.FindByNameAsync(per);
                if (person == null) { return null; }
                return await RepComment.AddComment(comment,person);
            }
            catch { throw; }
        }
        [HttpPut]
        [Route("BlocUser")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<bool> BlocUser(string phone, string ReasonForBan)
        {
            try 
            {
                Person? person = await UserM.FindByNameAsync(phone);
                if (person == null) { return false; }
                return await RepComment.BlocUser(person, ReasonForBan);
            }
            catch { throw; }
        }
        [HttpDelete]
        [Route("DeleteComment")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var per = UserM.GetUserId(User);
                if (per == null) { return false; }
                Person? person = await UserM.FindByNameAsync(per);
                if (person == null) { return false; }
                return await RepComment.Delete(id, person);
            }
            catch { throw; }
        }
        [HttpGet]
        [Route("GetComments")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<List<ViewComment>?> GetComments(Guid id)
        {
            try
            {
                return await RepComment.GetComments(id);
            }
            catch { throw; }
        }
        [HttpGet]
        [Route("GetReportComment")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<List<ViewComment>?> GetReportComment()
        {
            try
            {
                return await RepComment.GetReportComment();
            }
            catch { throw; }
        }
        [HttpPut]
        [Route("Report")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<bool> Report(Guid id)
        {
            try
            {
                return await RepComment.Report(id);
            }
            catch { throw; }
        }
        [HttpPut]
        [Route("UnBlocUser")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<bool> UnBlocUser(string phone)
        {
            try
            {
                Person? person = await UserM.FindByNameAsync(phone);
                if (person == null) { return false; }
                return await RepComment.UnBlocUser(person);
            }
            catch { throw; }
        }
        [HttpPut]
        [Route("UnReport")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<bool> UnReport(Guid id)
        {
            try
            {
                return await RepComment.UnReport(id);
            }
            catch { throw; }
        }
        [HttpPut]
        [Route("UpdateComment")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ViewComment?> UpdateComment(Guid id, string comment)
        {
            try
            {
                var per = UserM.GetUserId(User);
                if (per == null) { return null; }
                Person? person = await UserM.FindByNameAsync(per);
                if (person == null) { return null; }
                return await RepComment.UpdateComment(id,comment,person);
            }
            catch { throw; }
        }
    }
}
