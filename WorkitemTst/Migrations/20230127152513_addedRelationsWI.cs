using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkitemTst.Migrations
{
    /// <inheritdoc />
    public partial class addedRelationsWI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WIRelation_Workitems_TargetWorkitemId",
                table: "WIRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_WIRelation_Workitems_WorkitemId",
                table: "WIRelation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WIRelation",
                table: "WIRelation");

            migrationBuilder.RenameTable(
                name: "WIRelation",
                newName: "WIRelations");

            migrationBuilder.RenameIndex(
                name: "IX_WIRelation_WorkitemId",
                table: "WIRelations",
                newName: "IX_WIRelations_WorkitemId");

            migrationBuilder.RenameIndex(
                name: "IX_WIRelation_TargetWorkitemId",
                table: "WIRelations",
                newName: "IX_WIRelations_TargetWorkitemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WIRelations",
                table: "WIRelations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WIRelations_Workitems_TargetWorkitemId",
                table: "WIRelations",
                column: "TargetWorkitemId",
                principalTable: "Workitems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WIRelations_Workitems_WorkitemId",
                table: "WIRelations",
                column: "WorkitemId",
                principalTable: "Workitems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WIRelations_Workitems_TargetWorkitemId",
                table: "WIRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_WIRelations_Workitems_WorkitemId",
                table: "WIRelations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WIRelations",
                table: "WIRelations");

            migrationBuilder.RenameTable(
                name: "WIRelations",
                newName: "WIRelation");

            migrationBuilder.RenameIndex(
                name: "IX_WIRelations_WorkitemId",
                table: "WIRelation",
                newName: "IX_WIRelation_WorkitemId");

            migrationBuilder.RenameIndex(
                name: "IX_WIRelations_TargetWorkitemId",
                table: "WIRelation",
                newName: "IX_WIRelation_TargetWorkitemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WIRelation",
                table: "WIRelation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WIRelation_Workitems_TargetWorkitemId",
                table: "WIRelation",
                column: "TargetWorkitemId",
                principalTable: "Workitems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WIRelation_Workitems_WorkitemId",
                table: "WIRelation",
                column: "WorkitemId",
                principalTable: "Workitems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
