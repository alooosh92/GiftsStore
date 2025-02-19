using GiftsStore.Models;

namespace GiftsStore.Repository.Interface
{
    public interface IRepositoryComment <Add,View>
    {
        public Task<View?> AddComment(Add comment, Person person);
        public Task<List<View>?> GetComments(Guid id);
        public Task<View?> UpdateComment (Guid id, string comment,Person person);
        public Task<bool> Report (Guid id);
        public Task<bool> UnReport(Guid id);
        public Task<List<View>?> GetReportComment();
        public Task<bool> Delete(Guid id,Person person);
        public Task<bool> BlocUser(Person person,string ReasonForBan);
        public Task<bool> UnBlocUser(Person person);
    }
}
