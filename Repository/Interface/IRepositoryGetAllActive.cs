namespace GiftsStore.Repository.Interface
{
    public interface IRepositoryGetAllActive<T>
    {
        public Task<List<T>> GetAll(dynamic? element);
        public Task<bool> ActiveOrDelete(dynamic id);
    }
}
