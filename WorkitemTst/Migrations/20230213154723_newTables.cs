using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkitemTst.Migrations
{
    /// <inheritdoc />
    public partial class newTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_WorkitemType_WorkitemTypeId",
                table: "Fields");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkitemValue_Fields_FieldId",
                table: "WorkitemValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fields",
                table: "Fields");

            migrationBuilder.RenameTable(
                name: "Fields",
                newName: "WorkitemField");

            migrationBuilder.RenameIndex(
                name: "IX_Fields_WorkitemTypeId",
                table: "WorkitemField",
                newName: "IX_WorkitemField_WorkitemTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkitemField",
                table: "WorkitemField",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkitemField_WorkitemType_WorkitemTypeId",
                table: "WorkitemField",
                column: "WorkitemTypeId",
                principalTable: "WorkitemType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkitemValue_WorkitemField_FieldId",
                table: "WorkitemValue",
                column: "FieldId",
                principalTable: "WorkitemField",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkitemField_WorkitemType_WorkitemTypeId",
                table: "WorkitemField");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkitemValue_WorkitemField_FieldId",
                table: "WorkitemValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkitemField",
                table: "WorkitemField");

            migrationBuilder.RenameTable(
                name: "WorkitemField",
                newName: "Fields");

            migrationBuilder.RenameIndex(
                name: "IX_WorkitemField_WorkitemTypeId",
                table: "Fields",
                newName: "IX_Fields_WorkitemTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fields",
                table: "Fields",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_WorkitemType_WorkitemTypeId",
                table: "Fields",
                column: "WorkitemTypeId",
                principalTable: "WorkitemType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkitemValue_Fields_FieldId",
                table: "WorkitemValue",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
