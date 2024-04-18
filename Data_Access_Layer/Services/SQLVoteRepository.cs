using Data_Access_Layer.Data;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Services
{
    public class SQLVoteRepository : IVoteRepository
    {
        private readonly MyDBContext context;

        public SQLVoteRepository(MyDBContext context)
        {
            this.context = context;
        }

        //create
        public async Task<bool> CreateAsync(Vote vote)
        {
            try
            {
                await context.Vote.AddAsync(vote);
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

        //delete
        public async Task<bool> DeleteAsync(Vote vote)
        {
            try
            {
                context.Vote.Remove(vote);
                var result = await context.SaveChangesAsync();
                if( result == 1)
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
        public async Task<List<Vote>> GetAllAsync()
        {
            try
            {
                var allVote = await context.Vote.ToListAsync();
                return allVote;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get by id
        public async Task<Vote?> GetByIdAsync(Guid id)
        {
            try
            {
                var data = await context.Vote.FirstOrDefaultAsync(x => x.Id == id);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //update
        public async Task<bool> UpdateAsync(Vote vote)
        {
            try
            {
                context.Vote.Update(vote);
                var result = await context.SaveChangesAsync();
                if( result == 1)
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
