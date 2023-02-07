using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkitemTst.Migrations
{
    /// <inheritdoc />
    public partial class TransitionRemovedWorkflow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transition_Workflow_WorkflowId",
                table: "Transition");

            migrationBuilder.DropIndex(
                name: "IX_Transition_WorkflowId",
                table: "Transition");

            migrationBuilder.DropColumn(
                name: "WorkflowId",
                table: "Transition");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkflowId",
                table: "Transition",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transition_WorkflowId",
                table: "Transition",
                column: "WorkflowId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transition_Workflow_WorkflowId",
                table: "Transition",
                column: "WorkflowId",
                principalTable: "Workflow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
