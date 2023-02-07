using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkitemTst.Migrations
{
    /// <inheritdoc />
    public partial class addedEffortToWorkitemOneRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Effort_WorkitemId",
                table: "Effort");

            migrationBuilder.CreateIndex(
                name: "IX_Effort_WorkitemId",
                table: "Effort",
                column: "WorkitemId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Effort_WorkitemId",
                table: "Effort");

            migrationBuilder.CreateIndex(
                name: "IX_Effort_WorkitemId",
                table: "Effort",
                column: "WorkitemId");
        }
    }
}
