using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Catalogo.API.Migrations
{
    public partial class SoftDeleteProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Excluido",
                table: "Produtos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Excluido",
                table: "Produtos");
        }
    }
}
