using Microsoft.EntityFrameworkCore.Migrations;

namespace ElementAdmin.Infrastructure.Data.Migrations
{
    public partial class Init_20190530151659_avatar_to_text : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UIAvatar",
                table: "EAUserInfo",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 256);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UIAvatar",
                table: "EAUserInfo",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
