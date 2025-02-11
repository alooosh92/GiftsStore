using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GiftsStore.Repository.Interface
{
    public interface IRepositoryOffer<Add,View> 
    {
        public Task<View> Create(Add element);
        public Task<bool> Update(Guid element);
        public Task<View> Get(Guid id);
        public Task<List<View>> GetAll(Guid element);
    }
}
