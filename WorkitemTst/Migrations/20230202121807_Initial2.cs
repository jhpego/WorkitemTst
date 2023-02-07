using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkitemTst.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Interaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interaction_Interaction_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Interaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkitemType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkitemType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    FieldMode = table.Column<int>(type: "int", nullable: false),
                    WorkitemTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fields_WorkitemType_WorkitemTypeId",
                        column: x => x.WorkitemTypeId,
                        principalTable: "WorkitemType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Workitem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    WorkitemTypeId = table.Column<int>(type: "int", nullable: false),
                    InteractionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workitem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workitem_Interaction_InteractionId",
                        column: x => x.InteractionId,
                        principalTable: "Interaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Workitem_WorkitemType_WorkitemTypeId",
                        column: x => x.WorkitemTypeId,
                        principalTable: "WorkitemType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkitemTypeRelation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TargetWorkitemTypeId = table.Column<int>(type: "int", nullable: false),
                    SourceWorkitemTypeId = table.Column<int>(type: "int", nullable: false),
                    RelationMode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkitemTypeRelation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkitemTypeRelation_WorkitemType_SourceWorkitemTypeId",
                        column: x => x.SourceWorkitemTypeId,
                        principalTable: "WorkitemType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkitemTypeRelation_WorkitemType_TargetWorkitemTypeId",
                        column: x => x.TargetWorkitemTypeId,
                        principalTable: "WorkitemType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkitemValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FieldId = table.Column<int>(type: "int", nullable: false),
                    WorkitemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkitemValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkitemValue_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkitemValue_Workitem_WorkitemId",
                        column: x => x.WorkitemId,
                        principalTable: "Workitem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Worklog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkitemId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worklog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Worklog_Workitem_WorkitemId",
                        column: x => x.WorkitemId,
                        principalTable: "Workitem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkitemRelation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TargetWorkitemId = table.Column<int>(type: "int", nullable: false),
                    SourceWorkitemId = table.Column<int>(type: "int", nullable: false),
                    WorkitemTypeRelationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkitemRelation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkitemRelation_WorkitemTypeRelation_WorkitemTypeRelationId",
                        column: x => x.WorkitemTypeRelationId,
                        principalTable: "WorkitemTypeRelation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkitemRelation_Workitem_SourceWorkitemId",
                        column: x => x.SourceWorkitemId,
                        principalTable: "Workitem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkitemRelation_Workitem_TargetWorkitemId",
                        column: x => x.TargetWorkitemId,
                        principalTable: "Workitem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fields_WorkitemTypeId",
                table: "Fields",
                column: "WorkitemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Interaction_ParentId",
                table: "Interaction",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Workitem_InteractionId",
                table: "Workitem",
                column: "InteractionId");

            migrationBuilder.CreateIndex(
                name: "IX_Workitem_WorkitemTypeId",
                table: "Workitem",
                column: "WorkitemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkitemRelation_SourceWorkitemId",
                table: "WorkitemRelation",
                column: "SourceWorkitemId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkitemRelation_TargetWorkitemId",
                table: "WorkitemRelation",
                column: "TargetWorkitemId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkitemRelation_WorkitemTypeRelationId",
                table: "WorkitemRelation",
                column: "WorkitemTypeRelationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkitemTypeRelation_SourceWorkitemTypeId",
                table: "WorkitemTypeRelation",
                column: "SourceWorkitemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkitemTypeRelation_TargetWorkitemTypeId",
                table: "WorkitemTypeRelation",
                column: "TargetWorkitemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkitemValue_FieldId",
                table: "WorkitemValue",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkitemValue_WorkitemId",
                table: "WorkitemValue",
                column: "WorkitemId");

            migrationBuilder.CreateIndex(
                name: "IX_Worklog_WorkitemId",
                table: "Worklog",
                column: "WorkitemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkitemRelation");

            migrationBuilder.DropTable(
                name: "WorkitemValue");

            migrationBuilder.DropTable(
                name: "Worklog");

            migrationBuilder.DropTable(
                name: "WorkitemTypeRelation");

            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.DropTable(
                name: "Workitem");

            migrationBuilder.DropTable(
                name: "Interaction");

            migrationBuilder.DropTable(
                name: "WorkitemType");
        }
    }
}
