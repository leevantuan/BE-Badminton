using AutoMapper;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Services.Interface;
using Data_Transfer_Object.CommentDTO;
using Data_Transfer_Object.GetAll;
using Data_Transfer_Object.VoteDTO;
using Microsoft.AspNetCore.Identity;

namespace Business_Logic_Layer.CommentBLL
{
    public class SQLCommentRepositoryBLL : ICommentRepositoryBLL
    {
        private readonly ICommentRepository commentRepo;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;

        public SQLCommentRepositoryBLL(ICommentRepository commentRepo, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            this.commentRepo = commentRepo;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        //create
        public async Task<bool> CreateAsync(CommentRequestDTO comment)
        {
            var data = mapper.Map<Comment>(comment);
            return await commentRepo.CreateAsync(data);
        }

        //delete
        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await commentRepo.GetByIdAsync(id);
            if (result == null)
            {
                return false;
            }
            return await commentRepo.DeleteAsync(result);
        }

        //get all
        public async Task<List<GetComment>> GetAllAsync(GetAllRequestModel request)
        {
            var allComment = await commentRepo.GetAllAsync();

            if (string.IsNullOrWhiteSpace(request.FilterOn) == false && string.IsNullOrWhiteSpace(request.FilterQuery) == false)
            {
                if (request.FilterOn.Equals("Content", StringComparison.OrdinalIgnoreCase))
                {
                    //Contains lọc phân biệt chữ hoa chữ thường.
                    allComment = mapper.Map<List<Comment>>(allComment.Where(p => p.Content.ToLowerInvariant().Contains(request.FilterQuery.ToLowerInvariant())));
                }
            }

            if (string.IsNullOrWhiteSpace(request.SortBy) == false)
            {
                if (request.SortBy.Equals("Id", StringComparison.OrdinalIgnoreCase))
                {
                    allComment = mapper.Map<List<Comment>>(request.IsAcsending ? allComment.OrderBy(x => x.Id) : allComment.OrderByDescending(x => x.Id));
                }
            }
            var skipResult = (request.PageNumber - 1) * request.PageSize;

            var list = mapper.Map<List<GetComment>>(allComment.Skip(skipResult).Take(request.PageSize));

            return list;

        }

        //get by id
        public async Task<GetComment?> GetByIdAsync(Guid id)
        {
            var data = await commentRepo.GetByIdAsync(id);
            return mapper.Map<GetComment?>(data);
        }

        //update
        public async Task<bool> UpdateAsync(Guid id, CommentRequestDTO comment)
        {
            var result = mapper.Map<Comment>(comment);
            result.Id = id;
            return await commentRepo.UpdateAsync(result);
        }

        //get by product
        public async Task<List<GetCommentDetail>> GetByProductIdAsync(Guid productId)
        {
            var result = new List<GetCommentDetail>();

            var data = await commentRepo.GetAllAsync();
            var getProductId = data.Where(x => x.ProductId == productId);

            foreach (var comment in getProductId)
            {
                var user = await userManager.FindByIdAsync(comment.UserId);
                if (user == null)
                {
                    return result;
                }

                var newComment = mapper.Map<GetCommentDetail>(comment);
                newComment.UserName = user.UserName;

                result.Add(newComment);
            }
            return result;
        }
    }
}
