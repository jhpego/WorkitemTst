using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkitemTst.Migrations
{
    /// <inheritdoc />
    public partial class WorkitemUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WITypeId",
                table: "Workitems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "WIType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InternalCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WIType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WIRelation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WITypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WIRelation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WIRelation_WIType_WITypeId",
                        column: x => x.WITypeId,
                        principalTable: "WIType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workitems_WITypeId",
                table: "Workitems",
                column: "WITypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WIRelation_WITypeId",
                table: "WIRelation",
                column: "WITypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workitems_WIType_WITypeId",
                table: "Workitems",
                column: "WITypeId",
                principalTable: "WIType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workitems_WIType_WITypeId",
                table: "Workitems");

            migrationBuilder.DropTable(
                name: "WIRelation");

            migrationBuilder.DropTable(
                name: "WIType");

            migrationBuilder.DropIndex(
                name: "IX_Workitems_WITypeId",
                table: "Workitems");

            migrationBuilder.DropColumn(
                name: "WITypeId",
                table: "Workitems");
        }
    }
}
