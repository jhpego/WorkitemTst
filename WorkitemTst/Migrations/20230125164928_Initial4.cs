using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkitemTst.Migrations
{
    /// <inheritdoc />
    public partial class Initial4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WIValue_WIField_WIFieldId",
                table: "WIValue");

            migrationBuilder.DropForeignKey(
                name: "FK_Workitems_WIForm_FormId",
                table: "Workitems");

            migrationBuilder.DropIndex(
                name: "IX_Workitems_FormId",
                table: "Workitems");

            migrationBuilder.DropColumn(
                name: "FormId",
                table: "Workitems");

            migrationBuilder.RenameColumn(
                name: "WIFieldId",
                table: "WIValue",
                newName: "WorkitemId");

            migrationBuilder.RenameIndex(
                name: "IX_WIValue_WIFieldId",
                table: "WIValue",
                newName: "IX_WIValue_WorkitemId");

            migrationBuilder.AddColumn<int>(
                name: "FieldId",
                table: "WIValue",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FormId",
                table: "WIType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WIValue_FieldId",
                table: "WIValue",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_WIType_FormId",
                table: "WIType",
                column: "FormId");

            migrationBuilder.AddForeignKey(
                name: "FK_WIType_WIForm_FormId",
                table: "WIType",
                column: "FormId",
                principalTable: "WIForm",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WIValue_WIField_FieldId",
                table: "WIValue",
                column: "FieldId",
                principalTable: "WIField",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WIValue_Workitems_WorkitemId",
                table: "WIValue",
                column: "WorkitemId",
                principalTable: "Workitems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WIType_WIForm_FormId",
                table: "WIType");

            migrationBuilder.DropForeignKey(
                name: "FK_WIValue_WIField_FieldId",
                table: "WIValue");

            migrationBuilder.DropForeignKey(
                name: "FK_WIValue_Workitems_WorkitemId",
                table: "WIValue");

            migrationBuilder.DropIndex(
                name: "IX_WIValue_FieldId",
                table: "WIValue");

            migrationBuilder.DropIndex(
                name: "IX_WIType_FormId",
                table: "WIType");

            migrationBuilder.DropColumn(
                name: "FieldId",
                table: "WIValue");

            migrationBuilder.DropColumn(
                name: "FormId",
                table: "WIType");

            migrationBuilder.RenameColumn(
                name: "WorkitemId",
                table: "WIValue",
                newName: "WIFieldId");

            migrationBuilder.RenameIndex(
                name: "IX_WIValue_WorkitemId",
                table: "WIValue",
                newName: "IX_WIValue_WIFieldId");

            migrationBuilder.AddColumn<int>(
                name: "FormId",
                table: "Workitems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Workitems_FormId",
                table: "Workitems",
                column: "FormId");

            migrationBuilder.AddForeignKey(
                name: "FK_WIValue_WIField_WIFieldId",
                table: "WIValue",
                column: "WIFieldId",
                principalTable: "WIField",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Workitems_WIForm_FormId",
                table: "Workitems",
                column: "FormId",
                principalTable: "WIForm",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
