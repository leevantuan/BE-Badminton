using Business_Logic_Layer.AuthBLL;
using Business_Logic_Layer.BillBLL;
using Business_Logic_Layer.BookingBLL;
using Business_Logic_Layer.CategoryBLL;
using Business_Logic_Layer.CommentBLL;
using Business_Logic_Layer.CourtBLL;
using Business_Logic_Layer.OrderBLL;
using Business_Logic_Layer.ProductBillBLL;
using Business_Logic_Layer.ProductBLL;
using Business_Logic_Layer.ProductOrderBLL;
using Business_Logic_Layer.PurchaseOrderBLL;
using Business_Logic_Layer.SaleBLL;
using Business_Logic_Layer.SupplierBLL;
using Business_Logic_Layer.VoteBLL;
using Data_Access_Layer.Data;
using Data_Access_Layer.Services;
using Data_Access_Layer.Services.Auth;
using Data_Access_Layer.Services.Interface;
using Microsoft.EntityFrameworkCore;
using TheBookStore.Helper;

namespace Badminton.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration config)
        {

            services.AddDbContext<MyDBContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DemoBadminton"));
            });

            services.AddAutoMapper(typeof(ApplicationMapper));

            services.AddScoped<ITokenRepository, SQLTokenRepository>();
            services.AddScoped<IAuthRepository, SQLAuthRepository>();
            services.AddScoped<IAuthRepositoryBLL, SQLAuthRepositoryBLL>();

            //service
            services.AddScoped<ICourtRepository, SQLCourtRepository>();
            services.AddScoped<IBookingRepository, SQLBookingRepository>();
            services.AddScoped<IProductRepository, SQLProductRepository>();
            services.AddScoped<IPurchaseOrderRepository, SQLPurchaseOrderRepository>();
            services.AddScoped<ISupplierRepository, SQLSupplierRepository>();

            services.AddScoped<ICommentRepository, SQLCommentRepository>();
            services.AddScoped<IVoteRepository, SQLVoteRepository>();
            services.AddScoped<ISaleRepository, SQLSaleRepository>();
            services.AddScoped<IOrderRepository, SQLOrderRepository>();
            services.AddScoped<IProductOrderRepository, SQLProductOrderRepository>();
            services.AddScoped<IProductBillRepository, SQLProductBillRepository>();
            services.AddScoped<IBillRepository, SQLBillRepository>();
            services.AddScoped<ICategoryRepository, SQLCategoryRepository>();

            //bll
            services.AddScoped<ICourtRepositoryBLL, SQLCourtRepositoryBLL>();
            services.AddScoped<IBookingRepositoryBLL, SQLBookingRepositoryBLL>();
            services.AddScoped<IProductRepositoryBLL, SQLProductRepositoryBLL>();
            services.AddScoped<IPurchaseOrderRepositoryBLL, SQLPurchaseOrderRepositoryBLL>();
            services.AddScoped<ISupplierRepositoryBLL, SQLSupplierRepositoryBLL>();

            services.AddScoped<ICommentRepositoryBLL, SQLCommentRepositoryBLL>();
            services.AddScoped<IVoteRepositoryBLL, SQLVoteRepositoryBLL>();
            services.AddScoped<ISaleRepositoryBLL, SQLSaleRepositoryBLL>();
            services.AddScoped<IOrderRepositoryBLL, SQLOrderRepositoryBLL>();
            services.AddScoped<IProductOrderRepositoryBLL, SQLProductOrderRepositoryBLL>();
            services.AddScoped<IProductBillRepositoryBLL, SQLProductBillRepositoryBLL>();
            services.AddScoped<IBillRepositoryBLL, SQLBillRepositoryBLL>();
            services.AddScoped<ICategoryRepositoryBLL, SQLCategoryRepositoryBLL>();

            return services;
        }
    }
}
