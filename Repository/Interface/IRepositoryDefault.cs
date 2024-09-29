namespace GiftsStore.Repository.Interface
{
    public interface IRepositoryDefault<Add, View>: IRepositoryGetAllActive<View>
    {
        public Task<View> Create(Add element);
        public Task<View?> Update(View element);
        public Task<View?> Get(Guid id);       
    }
}
