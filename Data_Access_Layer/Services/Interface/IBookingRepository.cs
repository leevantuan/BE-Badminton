using Data_Access_Layer.Entities;

namespace Data_Access_Layer.Services.Interface
{
    public interface IBookingRepository
    {
        public Task<List<Booking>> GetAllAsync();

        public Task<Booking?> GetByIdAsync(Guid id);

        public Task<bool> CreateAsync(Booking booking);

        public Task<bool> UpdateAsync(Booking booking);

        public Task<bool> ChangeStatusAsync(Guid id,bool isStatus);

        public Task<bool> DeleteAsync(Booking booking);
    }
}
