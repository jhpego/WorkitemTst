using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkitemTst.Migrations
{
    /// <inheritdoc />
    public partial class PropertiesToList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WIField_WorkitemTypes_WITypeId",
                table: "WIField");

            migrationBuilder.DropForeignKey(
                name: "FK_WIValue_WIField_FieldId",
                table: "WIValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WIField",
                table: "WIField");

            migrationBuilder.RenameTable(
                name: "WIField",
                newName: "Fields");

            migrationBuilder.RenameIndex(
                name: "IX_WIField_WITypeId",
                table: "Fields",
                newName: "IX_Fields_WITypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fields",
                table: "Fields",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_WorkitemTypes_WITypeId",
                table: "Fields",
                column: "WITypeId",
                principalTable: "WorkitemTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WIValue_Fields_FieldId",
                table: "WIValue",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_WorkitemTypes_WITypeId",
                table: "Fields");

            migrationBuilder.DropForeignKey(
                name: "FK_WIValue_Fields_FieldId",
                table: "WIValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fields",
                table: "Fields");

            migrationBuilder.RenameTable(
                name: "Fields",
                newName: "WIField");

            migrationBuilder.RenameIndex(
                name: "IX_Fields_WITypeId",
                table: "WIField",
                newName: "IX_WIField_WITypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WIField",
                table: "WIField",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WIField_WorkitemTypes_WITypeId",
                table: "WIField",
                column: "WITypeId",
                principalTable: "WorkitemTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WIValue_WIField_FieldId",
                table: "WIValue",
                column: "FieldId",
                principalTable: "WIField",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
