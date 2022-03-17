using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sezona",
                columns: table => new
                {
                    SezonaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Godina = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sezona", x => x.SezonaID);
                });

            migrationBuilder.CreateTable(
                name: "Sudija",
                columns: table => new
                {
                    SudijaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sudija", x => x.SudijaID);
                });

            migrationBuilder.CreateTable(
                name: "Klub",
                columns: table => new
                {
                    KlubID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Grad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SezonaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klub", x => x.KlubID);
                    table.ForeignKey(
                        name: "FK_Klub_Sezona_SezonaID",
                        column: x => x.SezonaID,
                        principalTable: "Sezona",
                        principalColumn: "SezonaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Igrac",
                columns: table => new
                {
                    IgracID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    GodinaRodjenja = table.Column<int>(type: "int", nullable: false),
                    Nacionalnost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Golovi = table.Column<int>(type: "int", nullable: false),
                    Asistencije = table.Column<int>(type: "int", nullable: false),
                    KlubID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Igrac", x => x.IgracID);
                    table.ForeignKey(
                        name: "FK_Igrac_Klub_KlubID",
                        column: x => x.KlubID,
                        principalTable: "Klub",
                        principalColumn: "KlubID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Utakmica",
                columns: table => new
                {
                    MecID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SezonaID = table.Column<int>(type: "int", nullable: false),
                    Kolo = table.Column<int>(type: "int", nullable: false),
                    DomacinKlubID = table.Column<int>(type: "int", nullable: true),
                    golovi_domacin = table.Column<int>(type: "int", nullable: false),
                    GostKlubID = table.Column<int>(type: "int", nullable: true),
                    golovi_gost = table.Column<int>(type: "int", nullable: false),
                    SudijaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utakmica", x => x.MecID);
                    table.ForeignKey(
                        name: "FK_Utakmica_Klub_DomacinKlubID",
                        column: x => x.DomacinKlubID,
                        principalTable: "Klub",
                        principalColumn: "KlubID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Utakmica_Klub_GostKlubID",
                        column: x => x.GostKlubID,
                        principalTable: "Klub",
                        principalColumn: "KlubID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Utakmica_Sezona_SezonaID",
                        column: x => x.SezonaID,
                        principalTable: "Sezona",
                        principalColumn: "SezonaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Utakmica_Sudija_SudijaID",
                        column: x => x.SudijaID,
                        principalTable: "Sudija",
                        principalColumn: "SudijaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Igrac_KlubID",
                table: "Igrac",
                column: "KlubID");

            migrationBuilder.CreateIndex(
                name: "IX_Klub_SezonaID",
                table: "Klub",
                column: "SezonaID");

            migrationBuilder.CreateIndex(
                name: "IX_Utakmica_DomacinKlubID",
                table: "Utakmica",
                column: "DomacinKlubID");

            migrationBuilder.CreateIndex(
                name: "IX_Utakmica_GostKlubID",
                table: "Utakmica",
                column: "GostKlubID");

            migrationBuilder.CreateIndex(
                name: "IX_Utakmica_SezonaID",
                table: "Utakmica",
                column: "SezonaID");

            migrationBuilder.CreateIndex(
                name: "IX_Utakmica_SudijaID",
                table: "Utakmica",
                column: "SudijaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Igrac");

            migrationBuilder.DropTable(
                name: "Utakmica");

            migrationBuilder.DropTable(
                name: "Klub");

            migrationBuilder.DropTable(
                name: "Sudija");

            migrationBuilder.DropTable(
                name: "Sezona");
        }
    }
}
