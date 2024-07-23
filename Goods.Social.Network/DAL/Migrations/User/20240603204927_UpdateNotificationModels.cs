using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNotificationModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCheck",
                table: "Notifications");

            migrationBuilder.AddColumn<bool>(
                name: "IsCheck",
                table: "NotificationPosts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCheck",
                table: "NotificationChatRooms",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCheck",
                table: "NotificationPosts");

            migrationBuilder.DropColumn(
                name: "IsCheck",
                table: "NotificationChatRooms");

            migrationBuilder.AddColumn<bool>(
                name: "IsCheck",
                table: "Notifications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
