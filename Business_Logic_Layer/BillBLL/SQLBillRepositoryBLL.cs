using AutoMapper;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Services.Interface;
using Data_Transfer_Object.BillDTO;
using Data_Transfer_Object.GetAll;

namespace Business_Logic_Layer.BillBLL
{
    public class SQLBillRepositoryBLL : IBillRepositoryBLL
    {
        private readonly IBillRepository billRepo;
        private readonly IMapper mapper;

        public SQLBillRepositoryBLL(IBillRepository billRepo, IMapper mapper)
        {
            this.billRepo = billRepo;
            this.mapper = mapper;
        }

        //create
        public async Task<bool> CreateAsync(BillRequestDTO bill)
        {
            var data = mapper.Map<Bill>(bill);
            return await billRepo.CreateAsync(data);
        }

        //delete
        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await billRepo.GetByIdAsync(id);
            if (result == null)
            {
                return false;
            }
            return await billRepo.DeleteAsync(result);
        }

        //get all
        public async Task<List<GetBill>> GetAllAsync(GetAllDateTimeModel request)
        {
            var allBill = await billRepo.GetAllAsync();

            if (string.IsNullOrWhiteSpace(request.FilterOn) == false)
            {
                if (request.FilterOn.Equals("Date", StringComparison.OrdinalIgnoreCase))
                {
                    //Contains lọc phân biệt chữ hoa chữ thường.
                    allBill = mapper.Map<List<Bill>>(allBill.Where(p => p.CreateAt == request.FilterQuery));
                }
            }

            if (string.IsNullOrWhiteSpace(request.SortBy) == false)
            {
                if (request.SortBy.Equals("Status", StringComparison.OrdinalIgnoreCase))
                {
                    allBill = mapper.Map<List<Bill>>(request.IsAcsending ? allBill.OrderBy(x => x.IsStatus) : allBill.OrderByDescending(x => x.IsStatus));
                }
            }
            var skipResult = (request.PageNumber - 1) * request.PageSize;

            var list = mapper.Map<List<GetBill>>(allBill.Skip(skipResult).Take(request.PageSize));

            return list;
        }

        //get by id
        public async Task<GetBill?> GetByIdAsync(Guid id)
        {
            var data = await billRepo.GetByIdAsync(id);
            return mapper.Map<GetBill?>(data);
        }

        //get in date
        public async Task<List<GetBill>> GetByInDateAsync(FindDateTimeModel date)
        {
            var listData = await billRepo.GetAllAsync();
            var result = mapper.Map<List<GetBill>>(listData.Where(x => x.CreateAt == date.FindQuery));
            return result;
        }

        //get filter
        public async Task<List<GetBill>> GetByFilterInDateAsync(FilterDateTimeModel date)
        {
            var listData = await billRepo.GetAllAsync();
            var result = mapper.Map<List<GetBill>>(listData.Where(x => x.CreateAt >= date.StartTimeQuery && x.CreateAt <= date.EndTimeQuery));
            return result;
        }

        //get total in date
        public async Task<double> GetTotalByInDateAsync(FindDateTimeModel date)
        {
            double total = 0;
            var data = await GetByInDateAsync(date);
            foreach (var item in data)
            {
                total = total + item.Total;
            }
            return total;
        }

        //get total filter date
        public async Task<double> GetTotalByFilterInDateAsync(FilterDateTimeModel date)
        {
            double total = 0;
            var data = await GetByFilterInDateAsync(date);
            foreach (var item in data)
            {
                total = total + item.Total;
            }
            return total;
        }

        //get sale id
        public async Task<List<GetBill>> GetBySaleIdAsync(Guid saleId)
        {
            var data = await billRepo.GetAllAsync();
            var result = mapper.Map<List<GetBill>>(data.Where(x => x.SaleId == saleId));
            return result;
        }
    
    }
}
