using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IOT_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddSensorID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SensorId",
                table: "Room",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SensorId",
                table: "Room");
        }
    }
}
