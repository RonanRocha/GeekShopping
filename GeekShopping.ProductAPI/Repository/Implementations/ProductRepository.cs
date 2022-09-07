using AutoMapper;
using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Model;
using GeekShopping.ProductAPI.Model.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace GeekShopping.ProductAPI.Repository.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDataContext _ctx;
        private readonly IMapper _mapper;

        public ProductRepository(AppDataContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public async Task<ProductVO> Create(ProductVO productVo)
        {
            Product product = _mapper.Map<Product>(productVo);
            _ctx.Products.Add(product);
            await _ctx.SaveChangesAsync();
            return _mapper.Map<ProductVO>(product);
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                Product? product = await _ctx.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
                if (product == null) return false;
                _ctx.Products.Remove(product);
                await _ctx.SaveChangesAsync(true);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<ProductVO>> FindAll()
        {
            List<Product> products = await _ctx.Products.ToListAsync();
            return _mapper.Map<List<ProductVO>>(products);
        }

        public async Task<ProductVO> FindById(int id)
        {
            Product? product = await _ctx.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<ProductVO>(product);
        }

        public async Task<ProductVO> Update(ProductVO productVo)
        {
            Product product = _mapper.Map<Product>(productVo);
            _ctx.Products.Update(product);
            await _ctx.SaveChangesAsync();
            return _mapper.Map<ProductVO>(product);
        }
    }
}
