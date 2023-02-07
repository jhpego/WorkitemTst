using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkitemTst.Migrations
{
    /// <inheritdoc />
    public partial class ChangedWorkitemStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Workitem");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Workitem",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workitem_StatusId",
                table: "Workitem",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workitem_Status_StatusId",
                table: "Workitem",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workitem_Status_StatusId",
                table: "Workitem");

            migrationBuilder.DropIndex(
                name: "IX_Workitem_StatusId",
                table: "Workitem");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Workitem");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Workitem",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
