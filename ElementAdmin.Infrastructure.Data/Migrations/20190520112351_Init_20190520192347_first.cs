using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElementAdmin.Infrastructure.Data.Migrations
{
    public partial class Init_20190520192347_first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    RRRoleId = table.Column<long>(nullable: false),
                    RRRouteId = table.Column<long>(nullable: false),
                    RoleRouteEntity_RRRoleId = table.Column<long>(nullable: true),
                    RoleRouteEntity_RRRouteId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EARoleRoutes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EARoleRoutes_EARoles_RoleRouteEntity_RRRoleId",
                        column: x => x.RoleRouteEntity_RRRoleId,
                        principalTable: "EARoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EARoleRoutes_EARoutes_RoleRouteEntity_RRRouteId",
                        column: x => x.RoleRouteEntity_RRRouteId,
                        principalTable: "EARoutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EARoleRoutes_RoleRouteEntity_RRRoleId",
                table: "EARoleRoutes",
                column: "RoleRouteEntity_RRRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_EARoleRoutes_RoleRouteEntity_RRRouteId",
                table: "EARoleRoutes",
                column: "RoleRouteEntity_RRRouteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EARoleRoutes");

            migrationBuilder.DropTable(
                name: "EAUserInfo");

            migrationBuilder.DropTable(
                name: "EARoles");

            migrationBuilder.DropTable(
                name: "EARoutes");
        }
    }
}
