using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineFoodDelivery.Migrations
{
    /// <inheritdoc />
    public partial class bb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryAgent_User_UserId",
                table: "DeliveryAgent");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "DeliveryAgent",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_DeliveryAgent_UserId",
                table: "DeliveryAgent",
                newName: "IX_DeliveryAgent_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryAgent_User_Id",
                table: "DeliveryAgent",
                column: "Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryAgent_User_Id",
                table: "DeliveryAgent");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DeliveryAgent",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DeliveryAgent_Id",
                table: "DeliveryAgent",
                newName: "IX_DeliveryAgent_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryAgent_User_UserId",
                table: "DeliveryAgent",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
