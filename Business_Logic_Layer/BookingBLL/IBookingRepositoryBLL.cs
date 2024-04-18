using Data_Transfer_Object.BookingDTO;
using Data_Transfer_Object.GetAll;

namespace Business_Logic_Layer.BookingBLL
{
    public interface IBookingRepositoryBLL
    {
        public Task<List<GetBooking>> GetAllAsync(GetAllDateTimeModel request);

        public Task<GetBooking?> GetByIdAsync(Guid id);

        public Task<List<GetBookingDetail>> GetByInDateAsync(FindDateTimeModel date);

        public Task<List<GetBookingDetail>> GetByFilterInDateAsync(FilterDateTimeModel date);

        public Task<List<GetBookingDetail>> GetByCourtInDateAsync(Guid courtId, FindDateTimeModel date);

        public Task<List<GetBookingDetail>> GetByCustomerIdAsync(string customerId);

        public Task<bool> CreateAsync(BookingRequestDTO booking);

        public Task<bool> UpdateAsync(Guid id, BookingRequestDTO booking);

        public Task<bool> ChangeStatusAsync(Guid id, ChangeStatus isStatus);

        public Task<bool> DeleteAsync(Guid id);
    }
}
