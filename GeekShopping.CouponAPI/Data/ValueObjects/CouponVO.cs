using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GeekShopping.CouponAPI.Data.ValueObjects
{
    public class CouponVO
    {
        public int Id { get; set; }
        public string CouponCode { get; set; }

        public decimal DiscountAmount { get; set; }
    }
}
