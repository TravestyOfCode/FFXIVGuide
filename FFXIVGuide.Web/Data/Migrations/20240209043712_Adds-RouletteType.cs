using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FFXIVGuide.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddsRouletteType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RouletteTypeId",
                table: "Instance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RouletteType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouletteType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Instance_RouletteTypeId",
                table: "Instance",
                column: "RouletteTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RouletteType_Name",
                table: "RouletteType",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Instance_RouletteType_RouletteTypeId",
                table: "Instance",
                column: "RouletteTypeId",
                principalTable: "RouletteType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instance_RouletteType_RouletteTypeId",
                table: "Instance");

            migrationBuilder.DropTable(
                name: "RouletteType");

            migrationBuilder.DropIndex(
                name: "IX_Instance_RouletteTypeId",
                table: "Instance");

            migrationBuilder.DropColumn(
                name: "RouletteTypeId",
                table: "Instance");
        }
    }
}
