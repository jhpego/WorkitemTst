using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkitemTst.Migrations
{
    /// <inheritdoc />
    public partial class WIRelations4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Relation",
                table: "WIRelation",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TargetWorkitemId",
                table: "WIRelation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WIRelation_TargetWorkitemId",
                table: "WIRelation",
                column: "TargetWorkitemId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WIRelation_Workitems_TargetWorkitemId",
                table: "WIRelation",
                column: "TargetWorkitemId",
                principalTable: "Workitems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WIRelation_Workitems_TargetWorkitemId",
                table: "WIRelation");

            migrationBuilder.DropIndex(
                name: "IX_WIRelation_TargetWorkitemId",
                table: "WIRelation");

            migrationBuilder.DropColumn(
                name: "TargetWorkitemId",
                table: "WIRelation");

            migrationBuilder.AlterColumn<int>(
                name: "Relation",
                table: "WIRelation",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
