using Microsoft.EntityFrameworkCore.Migrations;

namespace Nomec1.Data.Migrations
{
    public partial class add_UserConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserConfig",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "varchar(450)", nullable: true),
                    UserCategoryId = table.Column<int>(nullable: false),
                    NickName = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConfig", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserConfig");
        }
    }
}
