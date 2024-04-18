using Data_Access_Layer.Data;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Services.Interface;
using Data_Transfer_Object.ProductDTO;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Services
{
    public class SQLProductRepository : IProductRepository
    {
        private readonly MyDBContext context;

        public SQLProductRepository(MyDBContext context)
        {
            this.context = context;
        }

        //change quantity
        public async Task<bool> InceaseQuantityAsync(Product product, ChangeQuantity quantity)
        {
            try
            {
                product.Quantity = product.Quantity + quantity.Quantity;

                var result = await context.SaveChangesAsync();
                if(result == 1)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ReduceQuantityAsync(Product product, ChangeQuantity quantity)
        {
            try
            {
                product.Quantity = product.Quantity - quantity.Quantity;

                var result = await context.SaveChangesAsync();
                if (result == 1)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //create
        public async Task<bool> CreateAsync(Product product)
        {
            try
            {
                await context.Product.AddAsync(product);
                var result = await context.SaveChangesAsync();
                if(result == 1)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //delete
        public async Task<bool> DeleteAsync(Product product)
        {
            try
            {
                context.Product.Remove(product);
                var result = await context.SaveChangesAsync();
                if(result == 1)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Get all
        public async Task<List<Product>> GetAllAsync()
        {
            try
            {
                var list = await context.Product.ToListAsync();

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get by id
        public async Task<Product?> GetByIdAsync(Guid id)
        {
            try
            {
                var result = new Product();
                result = await context.Product.FirstOrDefaultAsync(x => x.Id == id);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //update
        public async Task<bool> UpdateAsync(Product product)
        {
            try
            {               
                context.Update(product);
                var result = await context.SaveChangesAsync();
                if(result == 1)
                {
                    return true;
                }
                return false;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
