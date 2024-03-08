using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeriasApp.Data.Migrations
{
    public partial class initialsetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Info",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Requested = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    Confirmed = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    Days = table.Column<int>(type: "int", nullable: true),
                    Zone = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Info", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Info");
        }
    }
}
