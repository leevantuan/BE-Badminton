using Data_Access_Layer.Data;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Services
{
    public class SQLCommentRepository : ICommentRepository
    {
        private readonly MyDBContext context;

        public SQLCommentRepository(MyDBContext context)
        {
            this.context = context;
        }

        //create
        public async Task<bool> CreateAsync(Comment coment)
        {
            try
            {
                await context.Comment.AddAsync(coment);
                var result = await context.SaveChangesAsync();
                if(result == 1)
                {
                    return true;
                }
                return false;
            }
            catch(Exception)
            {
                throw;
            }
        }

        //delete
        public async Task<bool> DeleteAsync(Comment coment)
        {
            try
            {
                context.Comment.Remove(coment);
                var result = await context.SaveChangesAsync();
                if(result == 1)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get all
        public async Task<List<Comment>> GetAllAsync()
        {
            try
            {
                var allComment = await context.Comment.ToListAsync();
                return allComment;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get by id
        public async Task<Comment?> GetByIdAsync(Guid id)
        {
            try
            {
                var data = await context.Comment.FirstOrDefaultAsync(x => x.Id == id);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //update
        public async Task<bool> UpdateAsync(Comment coment)
        {
            try
            {
                context.Comment.Update(coment);
                var result = await context.SaveChangesAsync();
                if(result == 1)
                { return true; }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
