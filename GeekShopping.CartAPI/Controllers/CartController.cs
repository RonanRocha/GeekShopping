using GeekShopping.CartAPI.Data.ViewModels;
using GeekShopping.CartAPI.Messages;
using GeekShopping.CartAPI.RabbitMQSender;
using GeekShopping.CartAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CartAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {

        private ICartRepository _repository;
        private IRabbitMQMessageSender _rabbitMQMessageSender;

        public CartController(ICartRepository repository, IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _repository = repository ?? throw new
                ArgumentNullException(nameof(repository));

            _rabbitMQMessageSender = rabbitMQMessageSender;
        }

        [HttpGet("find-cart/{id}")]
        [Authorize]
        public async Task<ActionResult<CartViewModel>> FindById(string id)
        {
            var cart = await _repository.FindCartByUserId(id);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpPost("add-cart")]
        [Authorize]
        public async Task<ActionResult<CartViewModel>> AddCart([FromBody] CartViewModel vo)
        {
            var cart = await _repository.SaveOrUpdateCart(vo);
            if (cart == null) return NotFound();
            return Ok(cart);
        }


        [HttpPost("apply-coupon")]
        [Authorize]
        public async Task<ActionResult<CartViewModel>> ApplyCoupon([FromBody] CartViewModel cartViewModel)
        {
            string coupon = cartViewModel?.CartHeader?.CouponCode ?? String.Empty;
            string userId = cartViewModel?.CartHeader?.UserId ?? String.Empty;

            if(String.IsNullOrEmpty(coupon) || String.IsNullOrEmpty(userId))
            {
                return BadRequest();
            }

            var result = await _repository.ApplyCoupon(userId, coupon);
            if (!result) return NotFound();
            return Ok(result);
        }


        [HttpDelete("remove-coupon/{userId}")]
        [Authorize]
        public async Task<ActionResult<CartViewModel>> ApplyCoupon(string userId)
        {
          
            var result = await _repository.RemoveCoupon(userId);
            if (!result) return NotFound();
            return Ok(result);
        }


        [HttpPut("update-cart")]
        [Authorize]
        public async Task<ActionResult<CartViewModel>> UpdateCart([FromBody] CartViewModel vo)
        {
            var cart = await _repository.SaveOrUpdateCart(vo);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpDelete("remove-cart/{id}")]
        [Authorize]
        public async Task<ActionResult<CartViewModel>> RemoveCart(int id)
        {
            var status = await _repository.RemoveFromCart(id);
            if (!status) return BadRequest();
            return Ok(status);
        }

        [HttpPost("checkout")]
        [Authorize]
        public async Task<ActionResult<CheckoutHeaderVO>> Checkout(CheckoutHeaderVO checkoutVo)
        {

            if (checkoutVo?.UserId == null) return BadRequest();

            var cart = await _repository.FindCartByUserId(checkoutVo.UserId);
            if (cart == null) return NotFound();

            checkoutVo.CartDetails = cart.CartDetails;
            checkoutVo.DateTime = DateTime.UtcNow;

            _rabbitMQMessageSender.Send(checkoutVo, "checkoutQueue");
            //:todo RabbitMQ Logic

            return Ok(checkoutVo);
        }


    }
}
