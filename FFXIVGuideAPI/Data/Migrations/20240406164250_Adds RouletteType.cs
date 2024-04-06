using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FFXIVGuideAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddsRouletteType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RouletteType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouletteType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RouletteType_Name",
                table: "RouletteType",
                column: "Name",
                unique: true)
                .Annotation("SqlServer:Clustered", false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RouletteType");
        }
    }
}
