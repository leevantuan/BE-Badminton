using Data_Access_Layer.Entities;

namespace Data_Access_Layer.Services.Interface
{
    public interface ICommentRepository
    {
        public Task<List<Comment>> GetAllAsync();

        public Task<Comment?> GetByIdAsync(Guid id);

        public Task<bool> CreateAsync(Comment coment);

        public Task<bool> UpdateAsync(Comment coment);

        public Task<bool> DeleteAsync(Comment coment);
    }
}
