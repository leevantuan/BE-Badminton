using AutoMapper;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Services.Interface;
using Data_Transfer_Object.GetAll;
using Data_Transfer_Object.VoteDTO;
using Microsoft.AspNetCore.Identity;

namespace Business_Logic_Layer.VoteBLL
{
    public class SQLVoteRepositoryBLL : IVoteRepositoryBLL
    {
        private readonly IVoteRepository voteRepo;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;

        public SQLVoteRepositoryBLL(IVoteRepository voteRepo, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            this.voteRepo = voteRepo;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        //create
        public async Task<bool> CreateAsync(VoteRequestDTO vote)
        {
            var data = mapper.Map<Vote>(vote);
            return await voteRepo.CreateAsync(data);
        }

        //delete
        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await voteRepo.GetByIdAsync(id);
            if (result == null)
            {
                return false;
            }
            return await voteRepo.DeleteAsync(result);
        }

        //get all
        public async Task<List<GetVote>> GetAllAsync(GetAllRequestModel request)
        {
            var allVote = await voteRepo.GetAllAsync();

            if (string.IsNullOrWhiteSpace(request.SortBy) == false)
            {
                if (request.SortBy.Equals("VoteNumber", StringComparison.OrdinalIgnoreCase))
                {
                    allVote = mapper.Map<List<Vote>>(request.IsAcsending ? allVote.OrderBy(x => x.VoteNumber) : allVote.OrderByDescending(x => x.VoteNumber));
                }
            }
            var skipResult = (request.PageNumber - 1) * request.PageSize;

            var list = mapper.Map<List<GetVote>>(allVote.Skip(skipResult).Take(request.PageSize));

            return list;
        }

        //get by id
        public async Task<GetVote?> GetByIdAsync(Guid id)
        {
            var data = await voteRepo.GetByIdAsync(id);
            return mapper.Map<GetVote?>(data);
        }

        //update
        public async Task<bool> UpdateAsync(Guid id, VoteRequestDTO vote)
        {
            var result = mapper.Map<Vote>(vote);
            result.Id = id;
            return await voteRepo.UpdateAsync(result);
        }

        //get product
        public async Task<List<GetVoteDetail>> GetByProductIdAsync(Guid productId)
        {
            var result = new List<GetVoteDetail>();

            var allVote = await voteRepo.GetAllAsync();
            var data = allVote.Where(x => x.ProductId == productId);

            foreach (var vote in data)
            {
                var user = await userManager.FindByIdAsync(vote.UserId);
                if (user == null)
                {
                    return result; 
                }
                var resultVote = mapper.Map<GetVoteDetail>(vote);
                resultVote.UserName = user.UserName;

                result.Add(resultVote);
            }

            return result;
        }
    }
}
