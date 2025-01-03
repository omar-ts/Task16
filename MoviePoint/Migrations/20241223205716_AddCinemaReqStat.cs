using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviePoint.Migrations
{
    /// <inheritdoc />
    public partial class AddCinemaReqStat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "CinemaReqs",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "Pending");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "CinemaReqs");
        }
    }
}
