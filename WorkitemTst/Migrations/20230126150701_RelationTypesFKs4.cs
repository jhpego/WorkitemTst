using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkitemTst.Migrations
{
    /// <inheritdoc />
    public partial class RelationTypesFKs4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WITypeRelationId",
                table: "WorkitemTypes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkitemTypes_WITypeRelationId",
                table: "WorkitemTypes",
                column: "WITypeRelationId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkitemTypes_TypeRelations_WITypeRelationId",
                table: "WorkitemTypes",
                column: "WITypeRelationId",
                principalTable: "TypeRelations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkitemTypes_TypeRelations_WITypeRelationId",
                table: "WorkitemTypes");

            migrationBuilder.DropIndex(
                name: "IX_WorkitemTypes_WITypeRelationId",
                table: "WorkitemTypes");

            migrationBuilder.DropColumn(
                name: "WITypeRelationId",
                table: "WorkitemTypes");
        }
    }
}
