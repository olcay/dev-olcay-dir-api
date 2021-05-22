using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Persistence.Migrations
{
    public partial class Messaging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessageBoxes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PetId = table.Column<Guid>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: false),
                    CreatedById = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageBoxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageBoxes_Accounts_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageBoxes_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageBoxParticipants",
                columns: table => new
                {
                    AccountId = table.Column<int>(nullable: false),
                    MessageBoxId = table.Column<Guid>(nullable: false),
                    Read = table.Column<DateTimeOffset>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageBoxParticipants", x => new { x.AccountId, x.MessageBoxId });
                    table.ForeignKey(
                        name: "FK_MessageBoxParticipants_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageBoxParticipants_MessageBoxes_MessageBoxId",
                        column: x => x.MessageBoxId,
                        principalTable: "MessageBoxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MessageBoxId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    CreatedById = table.Column<int>(nullable: false),
                    Body = table.Column<string>(maxLength: 1000, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Accounts_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_MessageBoxes_MessageBoxId",
                        column: x => x.MessageBoxId,
                        principalTable: "MessageBoxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageBoxes_CreatedById",
                table: "MessageBoxes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_MessageBoxes_PetId",
                table: "MessageBoxes",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageBoxParticipants_MessageBoxId",
                table: "MessageBoxParticipants",
                column: "MessageBoxId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_CreatedById",
                table: "Messages",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MessageBoxId",
                table: "Messages",
                column: "MessageBoxId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageBoxParticipants");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "MessageBoxes");
        }
    }
}
