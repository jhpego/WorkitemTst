using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkitemTst.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "TargetWITypeId",
                table: "TypeRelations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TypeRelations_TargetWITypeId",
                table: "TypeRelations",
                column: "TargetWITypeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TypeRelations_WorkitemTypes_TargetWITypeId",
                table: "TypeRelations",
                column: "TargetWITypeId",
                principalTable: "WorkitemTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypeRelations_WorkitemTypes_TargetWITypeId",
                table: "TypeRelations");

            migrationBuilder.DropIndex(
                name: "IX_TypeRelations_TargetWITypeId",
                table: "TypeRelations");

            migrationBuilder.DropColumn(
                name: "TargetWITypeId",
                table: "TypeRelations");

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
    }
}
