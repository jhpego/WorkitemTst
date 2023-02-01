using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkitemTst.Migrations
{
    /// <inheritdoc />
    public partial class Initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SampleSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    identifier = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SampleSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WIForm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WIForm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WIField",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    FieldType = table.Column<int>(type: "int", nullable: false),
                    WIFormId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WIField", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WIField_WIForm_WIFormId",
                        column: x => x.WIFormId,
                        principalTable: "WIForm",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Workitems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workitems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workitems_WIForm_FormId",
                        column: x => x.FormId,
                        principalTable: "WIForm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WIValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WIFieldId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WIValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WIValue_WIField_WIFieldId",
                        column: x => x.WIFieldId,
                        principalTable: "WIField",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WIField_WIFormId",
                table: "WIField",
                column: "WIFormId");

            migrationBuilder.CreateIndex(
                name: "IX_WIValue_WIFieldId",
                table: "WIValue",
                column: "WIFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Workitems_FormId",
                table: "Workitems",
                column: "FormId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SampleSet");

            migrationBuilder.DropTable(
                name: "WIValue");

            migrationBuilder.DropTable(
                name: "Workitems");

            migrationBuilder.DropTable(
                name: "WIField");

            migrationBuilder.DropTable(
                name: "WIForm");
        }
    }
}
