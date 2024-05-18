using AutoMapper;
using Data_Access_Layer.Entities;
using Data_Transfer_Object.AuthDTO;
using Data_Transfer_Object.BillDTO;
using Data_Transfer_Object.BookingDTO;
using Data_Transfer_Object.CategoryDTO;
using Data_Transfer_Object.CommentDTO;
using Data_Transfer_Object.CourtDTO;
using Data_Transfer_Object.OrderDTO;
using Data_Transfer_Object.ProductBillDTO;
using Data_Transfer_Object.ProductDTO;
using Data_Transfer_Object.ProductOrderDTO;
using Data_Transfer_Object.PurchaseOrderDTO;
using Data_Transfer_Object.SaleDTO;
using Data_Transfer_Object.SupplierDTO;
using Data_Transfer_Object.VoteDTO;
using Microsoft.AspNetCore.Identity;

namespace TheBookStore.Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<IdentityUser, UserDTO>().ReverseMap();
            CreateMap<BookingRequestDTO, Booking>().ReverseMap();
            CreateMap<CourtRequestDTO, Court>().ReverseMap();

            CreateMap<GetBooking, Booking>().ReverseMap();
            CreateMap<GetBookingDetail, Booking>().ReverseMap();
            CreateMap<BookingUpdate, Booking>().ReverseMap();
            CreateMap<GetCourt, Court>().ReverseMap();

            CreateMap<ProductRequestDTO, Product>().ReverseMap();
            CreateMap<GetProduct, Product>().ReverseMap();

            CreateMap<PurchaseOrderRequestDTO, PurchaseOrder>().ReverseMap();
            CreateMap<GetPurchaseOrder, PurchaseOrder>().ReverseMap();
            CreateMap<GetPurchaseOrderDetail, PurchaseOrder>().ReverseMap();
            CreateMap<PurchaseOrderRequestDTO, ChangeQuantity>().ReverseMap();
            CreateMap<PurchaseOrder, ChangeQuantity>().ReverseMap();


            CreateMap<SupplierRequestDTO, Supplier>().ReverseMap();
            CreateMap<GetSupplier, Supplier>().ReverseMap();

            CreateMap<CommentRequestDTO, Comment>().ReverseMap();
            CreateMap<GetComment, Comment>().ReverseMap();
            CreateMap<GetCommentDetail, Comment>().ReverseMap();

            CreateMap<VoteRequestDTO, Vote>().ReverseMap();
            CreateMap<GetVote, Vote>().ReverseMap();
            CreateMap<GetVoteDetail, Vote>().ReverseMap();

            CreateMap<SaleRequestDTO, Sale>().ReverseMap();
            CreateMap<GetSale, Sale>().ReverseMap();

            CreateMap<OrderRequestDTO, Order>().ReverseMap();
            CreateMap<GetOrder, Order>().ReverseMap();
            CreateMap<GetOrderDetail, Order>().ReverseMap();

            CreateMap<ProductOrderRequestDTO, ProductOrder>().ReverseMap();
            CreateMap<GetProductOrder, ProductOrder>().ReverseMap();
            CreateMap<GetProductOrderDetail, ProductOrder>().ReverseMap();
            CreateMap<ChangeQuantity, ProductOrder>().ReverseMap();

            CreateMap<ProductBillRequestDTO, ProductBill>().ReverseMap();
            CreateMap<GetProductBill, ProductBill>().ReverseMap();
            CreateMap<GetProductBillDetail, ProductBill>().ReverseMap();
            CreateMap<ChangeQuantity, ProductBill>().ReverseMap();
            CreateMap<ChangeQuantity, ProductBillRequestDTO>().ReverseMap();

            CreateMap<BillRequestDTO, Bill>().ReverseMap();
            CreateMap<GetBill, Bill>().ReverseMap();

            CreateMap<CategoryRequestDTO, Category>().ReverseMap();
            CreateMap<GetCategory, Category>().ReverseMap();
            CreateMap<CategoryUpdate, Category>().ReverseMap();

            
        }
    }
}