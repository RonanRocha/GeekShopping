using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;
        public ProductController(IProductRepository repository)
        {
            _repository = repository ?? 
                throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            IEnumerable<ProductVO> products = await _repository.FindAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public  async Task<IActionResult> FindById(int id)
        {
            ProductVO product = await _repository.FindById(id);
            if(product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductVO productVO)
        {
            try
            {
                if (productVO == null) return BadRequest();
                ProductVO product = await _repository.Create(productVO);
                return Ok(product);

            }catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductVO productVO)
        {
            try
            {
                ProductVO product = await _repository.Update(productVO);
                return Ok(product);

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
           bool isDeleted = await _repository.Delete(id);
           if(!isDeleted) return BadRequest();
           return Ok(isDeleted);
        }
    }
}
