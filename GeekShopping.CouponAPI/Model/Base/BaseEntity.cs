using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GeekShopping.CouponAPI.Model.Base
{
    public class BaseEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
    }
}
