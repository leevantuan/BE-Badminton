using AutoMapper;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Services.Interface;
using Data_Transfer_Object.BookingDTO;
using Data_Transfer_Object.GetAll;
using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Business_Logic_Layer.BookingBLL
{
    public class SQLBookingRepositoryBLL : IBookingRepositoryBLL
    {
        private readonly IBookingRepository bookingRepo;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ICourtRepository courtRepo;

        public SQLBookingRepositoryBLL(IBookingRepository bookingRepo, IMapper mapper, UserManager<IdentityUser> userManager, ICourtRepository courtRepo)
        {
            this.bookingRepo = bookingRepo;
            this.mapper = mapper;
            this.userManager = userManager;
            this.courtRepo = courtRepo;
        }

        //change status
        public async Task<bool> ChangeStatusAsync(Guid id, ChangeStatus isStatus)
        {
            try
            {
                return await bookingRepo.ChangeStatusAsync(id, isStatus.IsStatus);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //create
        public async Task<bool> CreateAsync(BookingRequestDTO booking)
        {
            var data = mapper.Map<Booking>(booking);
            return await bookingRepo.CreateAsync(data);
        }

        //delete
        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await bookingRepo.GetByIdAsync(id);
            if (result == null)
            {
                return false;
            }
            return await bookingRepo.DeleteAsync(result);
        }

        //get all
        public async Task<List<GetBooking>> GetAllAsync(GetAllDateTimeModel request)
        {
            var allBooking = await bookingRepo.GetAllAsync();

            if (string.IsNullOrWhiteSpace(request.FilterOn) == false)
            {
                if (request.FilterOn.Equals("Date", StringComparison.OrdinalIgnoreCase))
                {
                    //Contains lọc phân biệt chữ hoa chữ thường.
                    allBooking = mapper.Map<List<Booking>>(allBooking.Where(p => p.BookingDate == request.FilterQuery));
                }
            }

            if (string.IsNullOrWhiteSpace(request.SortBy) == false)
            {
                if (request.SortBy.Equals("Status", StringComparison.OrdinalIgnoreCase))
                {
                    allBooking = mapper.Map<List<Booking>>(request.IsAcsending ? allBooking.OrderBy(x => x.IsStatus) : allBooking.OrderByDescending(x => x.IsStatus));
                }
            }
            var skipResult = (request.PageNumber - 1) * request.PageSize;

            var list = mapper.Map<List<GetBooking>>(allBooking.Skip(skipResult).Take(request.PageSize));

            return list;
        }

        //get by court in date
        public async Task<List<GetBookingDetail>> GetByCourtInDateAsync(Guid courtId, FindDateTimeModel date)
        {
            try
            {
                var result = new List<GetBookingDetail>();
                var data = await GetInDate(date);
                var bookingByCourt = data.Where(x => x.CourtId == courtId);
                foreach (var booking in bookingByCourt)
                {
                    var court = await courtRepo.GetByIdAsync(booking.CourtId);
                    var user = await userManager.FindByIdAsync(booking.UserId);
                    var newBooking = mapper.Map<GetBookingDetail>(booking);
                    newBooking.CourtName = court.Name;
                    newBooking.UserName = user.UserName;

                    result.Add(newBooking);
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get by filter date
        public async Task<List<GetBookingDetail>> GetByFilterInDateAsync(FilterDateTimeModel date)
        {
            try
            {
                var result = new List<GetBookingDetail>();
                var allBookings = await bookingRepo.GetAllAsync();
                var findBookings = allBookings.Where(x => x.BookingDate >= date.StartTimeQuery && x.BookingDate <= date.EndTimeQuery);
                foreach (var booking in findBookings)
                {
                    var court = await courtRepo.GetByIdAsync(booking.CourtId);
                    var user = await userManager.FindByIdAsync(booking.UserId);
                    var newBooking = mapper.Map<GetBookingDetail>(booking);
                    newBooking.CourtName = court.Name;
                    newBooking.UserName = user.UserName;

                    result.Add(newBooking);
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get by in date
        public async Task<List<GetBookingDetail>> GetByInDateAsync(FindDateTimeModel date)
        {
            try
            {
                var result = new List<GetBookingDetail>();
                var findBookings = await GetInDate(date);

                foreach (var booking in findBookings)
                {
                    var court = await courtRepo.GetByIdAsync(booking.CourtId);
                    var user = await userManager.FindByIdAsync(booking.UserId);
                    var newBooking = mapper.Map<GetBookingDetail>(booking);
                    newBooking.CourtName = court.Name;
                    newBooking.UserName = user.UserName;

                    result.Add(newBooking);
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get by customer
        public async Task<List<GetBookingDetail>> GetByCustomerIdAsync(string customerId)
        {
            try
            {
                var result = new List<GetBookingDetail>();
                var allBookings = await bookingRepo.GetAllAsync();
                var bookingByCustomer = allBookings.Where(x => x.UserId == customerId);

                foreach (var booking in bookingByCustomer)
                {
                    var court = await courtRepo.GetByIdAsync(booking.CourtId);
                    var user = await userManager.FindByIdAsync(booking.UserId);
                    var newBooking = mapper.Map<GetBookingDetail>(booking);
                    newBooking.CourtName = court.Name;
                    newBooking.UserName = user.UserName;

                    result.Add(newBooking);
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get in date
        private async Task<List<Booking>> GetInDate(FindDateTimeModel date)
        {
            var result = new List<Booking>();
            var allBookings = await bookingRepo.GetAllAsync();
            var findBookings = allBookings.Where(x => x.BookingDate == date.FindQuery);
            foreach (var booking in findBookings)
            {
                result.Add(booking);
            }
            return result;
        }

        //get by id
        public async Task<GetBooking?> GetByIdAsync(Guid id)
        {
            var dataBooking = await bookingRepo.GetByIdAsync(id);
            var result = mapper.Map<GetBooking>(dataBooking);
            return result;

        }

        //update
        public async Task<bool> UpdateAsync(Guid id, BookingRequestDTO booking)
        {
            var result = mapper.Map<Booking>(booking);
            result.Id = id;
            return await bookingRepo.UpdateAsync(result);
        }
    }
}
