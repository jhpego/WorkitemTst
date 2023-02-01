using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkitemTst.Migrations
{
    /// <inheritdoc />
    public partial class AddedFiledName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WIRelation_WIType_WITypeId",
                table: "WIRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_WIType_WIForm_FormId",
                table: "WIType");

            migrationBuilder.DropForeignKey(
                name: "FK_Workitems_WIType_WITypeId",
                table: "Workitems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WIType",
                table: "WIType");

            migrationBuilder.RenameTable(
                name: "WIType",
                newName: "WorkitemTypes");

            migrationBuilder.RenameIndex(
                name: "IX_WIType_FormId",
                table: "WorkitemTypes",
                newName: "IX_WorkitemTypes_FormId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "WIField",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkitemTypes",
                table: "WorkitemTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WIRelation_WorkitemTypes_WITypeId",
                table: "WIRelation",
                column: "WITypeId",
                principalTable: "WorkitemTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Workitems_WorkitemTypes_WITypeId",
                table: "Workitems",
                column: "WITypeId",
                principalTable: "WorkitemTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkitemTypes_WIForm_FormId",
                table: "WorkitemTypes",
                column: "FormId",
                principalTable: "WIForm",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WIRelation_WorkitemTypes_WITypeId",
                table: "WIRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_Workitems_WorkitemTypes_WITypeId",
                table: "Workitems");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkitemTypes_WIForm_FormId",
                table: "WorkitemTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkitemTypes",
                table: "WorkitemTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "WIField");

            migrationBuilder.RenameTable(
                name: "WorkitemTypes",
                newName: "WIType");

            migrationBuilder.RenameIndex(
                name: "IX_WorkitemTypes_FormId",
                table: "WIType",
                newName: "IX_WIType_FormId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WIType",
                table: "WIType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WIRelation_WIType_WITypeId",
                table: "WIRelation",
                column: "WITypeId",
                principalTable: "WIType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WIType_WIForm_FormId",
                table: "WIType",
                column: "FormId",
                principalTable: "WIForm",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workitems_WIType_WITypeId",
                table: "Workitems",
                column: "WITypeId",
                principalTable: "WIType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
