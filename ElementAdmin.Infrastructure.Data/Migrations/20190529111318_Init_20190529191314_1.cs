using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElementAdmin.Infrastructure.Data.Migrations
{
    public partial class Init_20190529191314_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EARoleRoutes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UpdateAt = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    DeleteAt = table.Column<DateTime>(nullable: false),
                    RRRoleKey = table.Column<string>(nullable: true),
                    RRRouteKey = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EARoleRoutes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EARoles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UpdateAt = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    DeleteAt = table.Column<DateTime>(nullable: false),
                    RRoleKey = table.Column<string>(maxLength: 16, nullable: false),
                    RName = table.Column<string>(maxLength: 256, nullable: false),
                    RDescription = table.Column<string>(maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EARoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EARoutes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UpdateAt = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    DeleteAt = table.Column<DateTime>(nullable: false),
                    RRouteKey = table.Column<string>(maxLength: 256, nullable: false),
                    RParentKey = table.Column<string>(maxLength: 256, nullable: false),
                    RName = table.Column<string>(maxLength: 256, nullable: false),
                    RSort = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EARoutes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EAUserInfo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UpdateAt = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    DeleteAt = table.Column<DateTime>(nullable: false),
                    UINickName = table.Column<string>(maxLength: 255, nullable: false),
                    UIUserName = table.Column<string>(maxLength: 16, nullable: false),
                    UIPassword = table.Column<string>(maxLength: 18, nullable: false),
                    UIRolesString = table.Column<string>(maxLength: 32, nullable: false),
                    UIToken = table.Column<Guid>(maxLength: 64, nullable: false),
                    UIIntroduction = table.Column<string>(maxLength: 256, nullable: false),
                    UIAvatar = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EAUserInfo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EARoleRoutes");

            migrationBuilder.DropTable(
                name: "EARoles");

            migrationBuilder.DropTable(
                name: "EARoutes");

            migrationBuilder.DropTable(
                name: "EAUserInfo");
        }
    }
}
