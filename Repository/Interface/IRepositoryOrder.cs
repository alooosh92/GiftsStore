namespace GiftsStore.Repository.Interface
{
    public interface IRepositoryOrder<Add, View>
    {
        public Task<View?> Create(Add element);

        public Task<bool> Delete(Guid id);

        public Task<View?> Get(Guid id);

        public Task<List<View>> GetAll(string id);
       
        public Task<bool> Verification(Guid id);

        public Task<bool> AppRoval(Guid id);

        public Task<bool> Ready(Guid id);

        public Task<bool> WaitingForDelivery(Guid id); 
        
        public Task<bool> Delivered(Guid id);
    }
}
