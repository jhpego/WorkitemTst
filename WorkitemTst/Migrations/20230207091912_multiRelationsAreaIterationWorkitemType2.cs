using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkitemTst.Migrations
{
    /// <inheritdoc />
    public partial class multiRelationsAreaIterationWorkitemType2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "Workitem",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentAreaIdId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Area_Area_ParentAreaIdId",
                        column: x => x.ParentAreaIdId,
                        principalTable: "Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workitem_AreaId",
                table: "Workitem",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Area_ParentAreaIdId",
                table: "Area",
                column: "ParentAreaIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workitem_Area_AreaId",
                table: "Workitem",
                column: "AreaId",
                principalTable: "Area",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workitem_Area_AreaId",
                table: "Workitem");

            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropIndex(
                name: "IX_Workitem_AreaId",
                table: "Workitem");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Workitem");
        }
    }
}
