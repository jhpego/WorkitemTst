using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkitemTst.Migrations
{
    /// <inheritdoc />
    public partial class RelationTypesFKs2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypeRelations_WorkitemTypes_TypeId",
                table: "TypeRelations");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "TypeRelations",
                newName: "WITypeId");

            migrationBuilder.RenameIndex(
                name: "IX_TypeRelations_TypeId",
                table: "TypeRelations",
                newName: "IX_TypeRelations_WITypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TypeRelations_WorkitemTypes_WITypeId",
                table: "TypeRelations",
                column: "WITypeId",
                principalTable: "WorkitemTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypeRelations_WorkitemTypes_WITypeId",
                table: "TypeRelations");

            migrationBuilder.RenameColumn(
                name: "WITypeId",
                table: "TypeRelations",
                newName: "TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_TypeRelations_WITypeId",
                table: "TypeRelations",
                newName: "IX_TypeRelations_TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TypeRelations_WorkitemTypes_TypeId",
                table: "TypeRelations",
                column: "TypeId",
                principalTable: "WorkitemTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
