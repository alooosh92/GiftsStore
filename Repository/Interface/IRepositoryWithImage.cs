namespace GiftsStore.Repository.Interface
{
    public interface IRepositoryWithImage <Add, View> :IRepositoryDefault<Add, View>
    {
        public Task<bool> DeleteImage(Guid id);
    }
}
