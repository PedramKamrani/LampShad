using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountMangment.Infracture.EFCore.Migrations
{
    public partial class AddActiveCodeTBAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActiveCode",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveCode",
                table: "Accounts");
        }
    }
}
