using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeekShopping.CouponAPI.Migrations
{
    public partial class seed_coupons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "coupon",
                columns: new[] { "id", "coupon_code", "discount_amount" },
                values: new object[,]
                {
                    { 1, "ERUDIO_2022_10", 10m },
                    { 2, "ERUDIO_2022_15", 15m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "coupon",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "coupon",
                keyColumn: "id",
                keyValue: 2);
        }
    }
}
