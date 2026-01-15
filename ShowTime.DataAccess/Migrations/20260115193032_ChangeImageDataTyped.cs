using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShowTime.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeImageDataTyped : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SplashArt",
                table: "Festival",
                type: "NVARCHAR(MAX)",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Artist",
                type: "NVARCHAR(MAX)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SplashArt",
                table: "Festival",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(MAX)",
                oldMaxLength: 1024);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Artist",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(MAX)",
                oldMaxLength: 1024,
                oldNullable: true);
        }
    }
}
