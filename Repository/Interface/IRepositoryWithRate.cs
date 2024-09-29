using GiftsStore.Models;

namespace GiftsStore.Repository.Interface
{
    public interface IRepositoryWithRate<Add,View>: IRepositoryWithImage<Add,View>
    {
        public Task<int?> Rate(Person user,Guid id,double rate);
    }
}
