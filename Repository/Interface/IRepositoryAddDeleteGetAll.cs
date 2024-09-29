namespace GiftsStore.Repository.Interface
{
    public interface IRepositoryAddDeleteGetAll<Add,View>
    {
        public Task<View> Create(Add element);
        public Task<bool> Delete(Guid id);
        public Task<List<View>> GetAll(dynamic id);
    }
}
