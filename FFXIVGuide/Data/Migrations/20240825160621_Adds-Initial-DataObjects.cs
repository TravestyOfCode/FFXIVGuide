using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FFXIVGuide.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddsInitialDataObjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InstanceType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstanceType", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Instance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstanceTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instance_InstanceType_InstanceTypeId",
                        column: x => x.InstanceTypeId,
                        principalTable: "InstanceType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Encounter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Ordinal = table.Column<int>(type: "int", nullable: false),
                    InstanceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encounter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Encounter_Instance_InstanceId",
                        column: x => x.InstanceId,
                        principalTable: "Instance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstanceRouletteType",
                columns: table => new
                {
                    InstancesId = table.Column<int>(type: "int", nullable: false),
                    RouletteTypesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstanceRouletteType", x => new { x.InstancesId, x.RouletteTypesId });
                    table.ForeignKey(
                        name: "FK_InstanceRouletteType_Instance_InstancesId",
                        column: x => x.InstancesId,
                        principalTable: "Instance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstanceRouletteType_RouletteType_RouletteTypesId",
                        column: x => x.RouletteTypesId,
                        principalTable: "RouletteType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EncounterNote",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ordinal = table.Column<int>(type: "int", nullable: false),
                    EncounterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncounterNote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EncounterNote_Encounter_EncounterId",
                        column: x => x.EncounterId,
                        principalTable: "Encounter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Encounter_InstanceId",
                table: "Encounter",
                column: "InstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Encounter_Name",
                table: "Encounter",
                column: "Name",
                unique: true)
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_EncounterNote_EncounterId",
                table: "EncounterNote",
                column: "EncounterId");

            migrationBuilder.CreateIndex(
                name: "IX_Instance_InstanceTypeId",
                table: "Instance",
                column: "InstanceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Instance_Name",
                table: "Instance",
                column: "Name",
                unique: true)
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_InstanceRouletteType_RouletteTypesId",
                table: "InstanceRouletteType",
                column: "RouletteTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_InstanceType_Name",
                table: "InstanceType",
                column: "Name",
                unique: true)
                .Annotation("SqlServer:Clustered", false);

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
                name: "EncounterNote");

            migrationBuilder.DropTable(
                name: "InstanceRouletteType");

            migrationBuilder.DropTable(
                name: "Encounter");

            migrationBuilder.DropTable(
                name: "RouletteType");

            migrationBuilder.DropTable(
                name: "Instance");

            migrationBuilder.DropTable(
                name: "InstanceType");
        }
    }
}
