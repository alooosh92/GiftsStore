namespace GiftsStore.Repository.Interface
{
    public interface IRepositoryOrder<Add, View>
    {
        public Task<View?> Create(Add element);

        public Task<bool> Delete(Guid id);

        public Task<View?> Get(Guid id);

        public Task<List<View>> GetAll(string id);

    }
}
