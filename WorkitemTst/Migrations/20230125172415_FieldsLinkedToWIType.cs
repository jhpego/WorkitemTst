using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkitemTst.Migrations
{
    /// <inheritdoc />
    public partial class FieldsLinkedToWIType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WIField_WIForm_WIFormId",
                table: "WIField");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkitemTypes_WIForm_FormId",
                table: "WorkitemTypes");

            migrationBuilder.DropTable(
                name: "WIForm");

            migrationBuilder.DropIndex(
                name: "IX_WorkitemTypes_FormId",
                table: "WorkitemTypes");

            migrationBuilder.DropColumn(
                name: "FormId",
                table: "WorkitemTypes");

            migrationBuilder.RenameColumn(
                name: "WIFormId",
                table: "WIField",
                newName: "WITypeId");

            migrationBuilder.RenameIndex(
                name: "IX_WIField_WIFormId",
                table: "WIField",
                newName: "IX_WIField_WITypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_WIField_WorkitemTypes_WITypeId",
                table: "WIField",
                column: "WITypeId",
                principalTable: "WorkitemTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WIField_WorkitemTypes_WITypeId",
                table: "WIField");

            migrationBuilder.RenameColumn(
                name: "WITypeId",
                table: "WIField",
                newName: "WIFormId");

            migrationBuilder.RenameIndex(
                name: "IX_WIField_WITypeId",
                table: "WIField",
                newName: "IX_WIField_WIFormId");

            migrationBuilder.AddColumn<int>(
                name: "FormId",
                table: "WorkitemTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_WorkitemTypes_FormId",
                table: "WorkitemTypes",
                column: "FormId");

            migrationBuilder.AddForeignKey(
                name: "FK_WIField_WIForm_WIFormId",
                table: "WIField",
                column: "WIFormId",
                principalTable: "WIForm",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkitemTypes_WIForm_FormId",
                table: "WorkitemTypes",
                column: "FormId",
                principalTable: "WIForm",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
