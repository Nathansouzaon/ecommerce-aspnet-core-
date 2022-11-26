using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Pedidos.Infra.Migrations
{
    public partial class SoftDeleteVoucher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Excluido",
                table: "Vouchers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Excluido",
                table: "Vouchers");
        }
    }
}
