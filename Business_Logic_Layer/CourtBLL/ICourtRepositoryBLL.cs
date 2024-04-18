using Data_Transfer_Object.CourtDTO;
using Data_Transfer_Object.GetAll;

namespace Business_Logic_Layer.CourtBLL
{
    public interface ICourtRepositoryBLL
    {
        public Task<List<GetCourt>> GetAllAsync(GetAllRequestModel request);

        public Task<GetCourt?> GetByIdAsync(Guid id);

        public Task<bool> CreateAsync(CourtRequestDTO court);

        public Task<bool> UpdateAsync(Guid id, CourtRequestDTO court);

        public Task<bool> DeleteAsync(Guid id);
    }
}
