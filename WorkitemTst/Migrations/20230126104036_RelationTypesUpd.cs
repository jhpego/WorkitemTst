using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkitemTst.Migrations
{
    /// <inheritdoc />
    public partial class RelationTypesUpd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WITypeRelation_WorkitemTypes_TypeId",
                table: "WITypeRelation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WITypeRelation",
                table: "WITypeRelation");

            migrationBuilder.RenameTable(
                name: "WITypeRelation",
                newName: "TypeRelations");

            migrationBuilder.RenameIndex(
                name: "IX_WITypeRelation_TypeId",
                table: "TypeRelations",
                newName: "IX_TypeRelations_TypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeRelations",
                table: "TypeRelations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TypeRelations_WorkitemTypes_TypeId",
                table: "TypeRelations",
                column: "TypeId",
                principalTable: "WorkitemTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypeRelations_WorkitemTypes_TypeId",
                table: "TypeRelations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeRelations",
                table: "TypeRelations");

            migrationBuilder.RenameTable(
                name: "TypeRelations",
                newName: "WITypeRelation");

            migrationBuilder.RenameIndex(
                name: "IX_TypeRelations_TypeId",
                table: "WITypeRelation",
                newName: "IX_WITypeRelation_TypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WITypeRelation",
                table: "WITypeRelation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WITypeRelation_WorkitemTypes_TypeId",
                table: "WITypeRelation",
                column: "TypeId",
                principalTable: "WorkitemTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
