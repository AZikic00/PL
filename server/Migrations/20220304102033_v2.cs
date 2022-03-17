using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Klub_Sezona_SezonaID",
                table: "Klub");

            migrationBuilder.DropForeignKey(
                name: "FK_Utakmica_Sudija_SudijaID",
                table: "Utakmica");

            migrationBuilder.AlterColumn<int>(
                name: "SudijaID",
                table: "Utakmica",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SezonaID",
                table: "Klub",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Klub_Sezona_SezonaID",
                table: "Klub",
                column: "SezonaID",
                principalTable: "Sezona",
                principalColumn: "SezonaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Utakmica_Sudija_SudijaID",
                table: "Utakmica",
                column: "SudijaID",
                principalTable: "Sudija",
                principalColumn: "SudijaID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Klub_Sezona_SezonaID",
                table: "Klub");

            migrationBuilder.DropForeignKey(
                name: "FK_Utakmica_Sudija_SudijaID",
                table: "Utakmica");

            migrationBuilder.AlterColumn<int>(
                name: "SudijaID",
                table: "Utakmica",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SezonaID",
                table: "Klub",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Klub_Sezona_SezonaID",
                table: "Klub",
                column: "SezonaID",
                principalTable: "Sezona",
                principalColumn: "SezonaID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Utakmica_Sudija_SudijaID",
                table: "Utakmica",
                column: "SudijaID",
                principalTable: "Sudija",
                principalColumn: "SudijaID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
