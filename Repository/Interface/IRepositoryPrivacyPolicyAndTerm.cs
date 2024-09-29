using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GiftsStore.Repository.Interface
{
    public interface IRepositoryPrivacyPolicyAndTerm <Add,View>
    {
        public Task<bool> Create(Add element);
        public Task<bool> Delete(int id);
        public Task<List<View>> GetAll();
    }
}
