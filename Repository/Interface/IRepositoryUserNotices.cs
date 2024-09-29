using GiftsStore.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GiftsStore.Repository.Interface
{
    public interface IRepositoryUserNotices<View>
    {
        public Task<bool> Create(string title,string message,Person person);
        public Task<int> GetNumUnRead(Person person);
        public Task<List<View>> Getfirst50(Person person);
    }
}
