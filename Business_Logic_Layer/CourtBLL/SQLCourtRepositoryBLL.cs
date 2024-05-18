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
        public async Task<List<GetCourt>> GetAllAsync()
        {
            var allCourt = await courtRepo.GetAllAsync();

            var list = mapper.Map<List<GetCourt>>(allCourt);
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
