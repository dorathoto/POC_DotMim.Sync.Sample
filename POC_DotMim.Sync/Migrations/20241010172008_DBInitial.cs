using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POC_DotMim.Sync.Migrations
{
    /// <inheritdoc />
    public partial class DBInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LedEffect",
                columns: table => new
                {
                    LedEffectId = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedEffect", x => x.LedEffectId);
                });

            migrationBuilder.CreateTable(
                name: "Tennants",
                columns: table => new
                {
                    TennantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tennants", x => x.TennantId);
                });

            migrationBuilder.CreateTable(
                name: "Audios",
                columns: table => new
                {
                    AudioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TennantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audios", x => x.AudioId);
                    table.ForeignKey(
                        name: "FK_Audios_Tennants_TennantId",
                        column: x => x.TennantId,
                        principalTable: "Tennants",
                        principalColumn: "TennantId");
                });

            migrationBuilder.CreateTable(
                name: "Led",
                columns: table => new
                {
                    LedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TennantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LedEffectId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Led", x => x.LedId);
                    table.ForeignKey(
                        name: "FK_Led_LedEffect_LedEffectId",
                        column: x => x.LedEffectId,
                        principalTable: "LedEffect",
                        principalColumn: "LedEffectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Led_Tennants_TennantId",
                        column: x => x.TennantId,
                        principalTable: "Tennants",
                        principalColumn: "TennantId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Audios_TennantId",
                table: "Audios",
                column: "TennantId");

            migrationBuilder.CreateIndex(
                name: "IX_Led_LedEffectId",
                table: "Led",
                column: "LedEffectId");

            migrationBuilder.CreateIndex(
                name: "IX_Led_TennantId",
                table: "Led",
                column: "TennantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audios");

            migrationBuilder.DropTable(
                name: "Led");

            migrationBuilder.DropTable(
                name: "LedEffect");

            migrationBuilder.DropTable(
                name: "Tennants");
        }
    }
}
