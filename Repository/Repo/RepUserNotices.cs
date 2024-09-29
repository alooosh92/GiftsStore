using GiftsStore.Data;
using GiftsStore.DataModels.UserNotices;
using GiftsStore.Models;
using GiftsStore.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GiftsStore.Repository.Repo
{
    public class RepUserNotices : IRepositoryUserNotices<ViewUserNotices>
    {
        public RepUserNotices(ApplicationDbContext db)
        {
            Db = db;
        }

        public ApplicationDbContext Db { get; }

        public async Task<bool> Create(string title, string message, Person person)
        {
            try
            {
                UserNotices userNotices = new() { 
                    CreateDate = DateTime.Now,
                    Id = Guid.NewGuid(),
                    IsRead = false,
                    Message = message,
                    Person = person,
                    Title = title,
                };
                await Db.UserNotices.AddAsync(userNotices);
                await Db.SaveChangesAsync();
                return true;
            }
            catch { throw; }
        }

        public async Task<List<ViewUserNotices>> Getfirst50(Person person)
        {
            try
            {
                List<UserNotices> userNotices =await Db.UserNotices.Include(p=>p.Person).Where(a=>a.Person!.Id == person.Id).ToListAsync();
                int num = await GetNumUnRead(person);
                if( num < 40) { num = 50; } else { num += 10; }
                userNotices = userNotices.OrderByDescending(a=>a.CreateDate).Take(num).ToList();
                List<ViewUserNotices> vList = new();
                foreach (var item in userNotices)
                {
                    vList.Add(item.ToViewUserNotices());
                    if (!item.IsRead)
                    {
                        item.IsRead = true;
                        Db.UserNotices.Update(item);
                    }
                }
                await Db.SaveChangesAsync();
                return vList;
            }
            catch { throw; }
        }

        public async Task<int> GetNumUnRead(Person person)
        {
            try
            {
                List<UserNotices> userNotices = await Db.UserNotices.Include(p => p.Person).Where(a => a.Person!.Id == person.Id && !a.IsRead).ToListAsync();
                return userNotices.Count;
            }
            catch
            {
                throw;
            }
        }
    }
}
