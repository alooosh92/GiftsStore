using GiftsStore.Data;
using GiftsStore.DataModels.CommentData;
using GiftsStore.Models;
using GiftsStore.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GiftsStore.Repository.Repo
{
    public class RepComment : IRepositoryComment<AddComment, ViewComment>
    {
        public RepComment(ApplicationDbContext DB,UserManager<Person> userManager)
        {
            this.DB = DB;
            UserManager = userManager;
        }

        public ApplicationDbContext DB { get; }
        public UserManager<Person> UserManager { get; }

        public async Task<ViewComment?> AddComment(AddComment comment, Person person)
        {
            try
            {
                Comment com = comment.ToComment(person);
                com.Store = await DB.Stores.SingleOrDefaultAsync(a => a.Id == com.Store!.Id);
                if (com.Store == null || person.CanComment == false) { return null; }
                await DB.Comments.AddAsync(com);
                await DB.SaveChangesAsync();
                return com.ToViewComment();
            }
            catch { throw; }
        }

        public async Task<bool> BlocUser(Person person, string ReasonForBan)
        {
            try
            {
                person.CanComment = false;
                person.ReasonForBan = ReasonForBan;
                DB.Users.Update(person);
                await DB.SaveChangesAsync();
                return true;
            }
            catch { throw; }
        }

        public async Task<bool> Delete(Guid id, Person person)
        {
            try
            {
                Comment? comment = await DB.Comments.Include(a=>a.Person).SingleOrDefaultAsync(a => a.Id == id);
                if(comment == null) { return false; }
                if(!await UserManager.IsInRoleAsync(person, "Admin") && comment.Person!.PhoneNumber != person.PhoneNumber) { return false; }
                DB.Comments.Remove(comment);
                await DB.SaveChangesAsync();
                return true;
            }
            catch { throw; }
        }

        public async Task<List<ViewComment>?> GetComments(Guid id)
        {
            try
            {
                List<Comment>? comments = await DB.Comments.Include(a=>a.Store).Include(a=>a.Person).Where(a=>a.Store!.Id==id).ToListAsync();
                if (comments == null) { return null; }
                List<ViewComment> viewComments = [];
                foreach (var item in comments)
                {
                    viewComments.Add(item.ToViewComment());
                }
                return viewComments;
            }
            catch { throw; }
        }

        public async Task<List<ViewComment>?> GetReportComment()
        {
            try
            {
                List<Comment>? comments = await DB.Comments.Include(a => a.Store).Include(a => a.Person).Where(a => a.Report).ToListAsync();
                if (comments == null) { return null; }
                List<ViewComment> viewComments = [];
                foreach (var item in comments)
                {
                    viewComments.Add(item.ToViewComment());
                }
                return viewComments;
            }
            catch { throw; }
        }

        public async Task<bool> Report(Guid id)
        {
            try
            {
                Comment? comment = await DB.Comments.Include(a=>a.Store).Include(a=>a.Person).SingleOrDefaultAsync(a => a.Id == id);
                if (comment == null) { return false; }
                comment.Report = true;
                DB.Comments.Update(comment);
                await DB.SaveChangesAsync();
                return true;
            }
            catch { throw; }
        }

        public async Task<bool> UnBlocUser(Person person)
        {
            try
            {
                person.CanComment = true;
                DB.Users.Update(person);
                await DB.SaveChangesAsync();
                return true;
            }
            catch { throw; }
        }

        public async Task<bool> UnReport(Guid id)
        {
            try
            {
                Comment? comment = await DB.Comments.Include(a => a.Store).Include(a => a.Person).SingleOrDefaultAsync(a => a.Id == id);
                if (comment == null) { return false; }
                comment.Report = false;
                DB.Comments.Update(comment);
                await DB.SaveChangesAsync();
                return true;
            }
            catch { throw; }
        }

        public async Task<ViewComment?> UpdateComment(Guid id, string comment, Person person)
        {
            try
            {
                Comment? com = await DB.Comments.Include(a => a.Store).Include(a => a.Person).SingleOrDefaultAsync(a => a.Id == id);
                if (com == null) { return null; }
                if(com.Person != person) { return null; }
                com.Text = comment;
                DB.Comments.Update(com);
                await DB.SaveChangesAsync();
                return com.ToViewComment();
            }
            catch { throw; }
        }
    }
}
