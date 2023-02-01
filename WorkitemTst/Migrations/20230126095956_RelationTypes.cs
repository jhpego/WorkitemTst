using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkitemTst.Migrations
{
    /// <inheritdoc />
    public partial class RelationTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WIRelation_WorkitemTypes_WITypeId",
                table: "WIRelation");

            migrationBuilder.DropIndex(
                name: "IX_WIRelation_WITypeId",
                table: "WIRelation");

            migrationBuilder.DropColumn(
                name: "WITypeId",
                table: "WIRelation");

            migrationBuilder.AddColumn<int>(
                name: "Relation",
                table: "WIRelation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkitemId",
                table: "WIRelation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "WITypeRelation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    Relation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WITypeRelation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WITypeRelation_WorkitemTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "WorkitemTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WIRelation_WorkitemId",
                table: "WIRelation",
                column: "WorkitemId");

            migrationBuilder.CreateIndex(
                name: "IX_WITypeRelation_TypeId",
                table: "WITypeRelation",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_WIRelation_Workitems_WorkitemId",
                table: "WIRelation",
                column: "WorkitemId",
                principalTable: "Workitems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WIRelation_Workitems_WorkitemId",
                table: "WIRelation");

            migrationBuilder.DropTable(
                name: "WITypeRelation");

            migrationBuilder.DropIndex(
                name: "IX_WIRelation_WorkitemId",
                table: "WIRelation");

            migrationBuilder.DropColumn(
                name: "Relation",
                table: "WIRelation");

            migrationBuilder.DropColumn(
                name: "WorkitemId",
                table: "WIRelation");

            migrationBuilder.AddColumn<int>(
                name: "WITypeId",
                table: "WIRelation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WIRelation_WITypeId",
                table: "WIRelation",
                column: "WITypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_WIRelation_WorkitemTypes_WITypeId",
                table: "WIRelation",
                column: "WITypeId",
                principalTable: "WorkitemTypes",
                principalColumn: "Id");
        }
    }
}
