using AutoMapper;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Services.Interface;
using Data_Transfer_Object.CourtDTO;
using Data_Transfer_Object.GetAll;

namespace Business_Logic_Layer.CourtBLL
{
    public class SQLCourtRepositoryBLL : ICourtRepositoryBLL
    {
        private readonly ICourtRepository courtRepo;
        private readonly IMapper mapper;

        public SQLCourtRepositoryBLL(ICourtRepository courtRepo, IMapper mapper)
        {
            this.courtRepo = courtRepo;
            this.mapper = mapper;
        }

        //create
        public async Task<bool> CreateAsync(CourtRequestDTO court)
        {
            var data = mapper.Map<Court>(court);
            return await courtRepo.CreateAsync(data);
        }

        //delete
        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await courtRepo.GetByIdAsync(id);
            if (result == null)
            {
                return false;
            }
            return await courtRepo.DeleteAsync(result);
        }

        //get all
        public async Task<List<GetCourt>> GetAllAsync(GetAllRequestModel request)
        {
            var allCourt = await courtRepo.GetAllAsync();

            if (string.IsNullOrWhiteSpace(request.FilterOn) == false && string.IsNullOrWhiteSpace(request.FilterQuery) == false)
            {
                if (request.FilterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    //Contains lọc phân biệt chữ hoa chữ thường.
                    allCourt = mapper.Map<List<Court>>(allCourt.Where(p => p.Name.ToLowerInvariant().Contains(request.FilterQuery.ToLowerInvariant())));
                }
            }

            if (string.IsNullOrWhiteSpace(request.SortBy) == false)
            {
                if (request.SortBy.Equals("Status", StringComparison.OrdinalIgnoreCase))
                {
                    allCourt = mapper.Map<List<Court>>(request.IsAcsending ? allCourt.OrderBy(x => x.IsStatus) : allCourt.OrderByDescending(x => x.IsStatus));
                }
            }
            var skipResult = (request.PageNumber - 1) * request.PageSize;
            var list = mapper.Map<List<GetCourt>>(allCourt.Skip(skipResult).Take(request.PageSize));
            return list;
        }

        //get by id
        public async Task<GetCourt?> GetByIdAsync(Guid id)
        {
            var result = await courtRepo.GetByIdAsync(id);
            return mapper.Map<GetCourt?>(result);
        }

        //update
        public async Task<bool> UpdateAsync(Guid id, CourtRequestDTO court)
        {
            var result = mapper.Map<Court>(court);
            result.Id = id;
            return await courtRepo.UpdateAsync(result);
        }
    }
}
