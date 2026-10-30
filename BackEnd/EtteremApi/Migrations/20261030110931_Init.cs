using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EtteremApi.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "asztalok",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Szam = table.Column<int>(type: "int", nullable: false),
                    Ferohely = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asztalok", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "felhasznalok",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nev = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    JelszoHash = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Telefon = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    Szerepkor = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    RegisztracioDatuma = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_felhasznalok", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "kiadas_kategoriak",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nev = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kiadas_kategoriak", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "alkalmazottak",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FelhasznaloId = table.Column<int>(type: "int", nullable: false),
                    Munkakor = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    Ber = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    BelepesDatum = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alkalmazottak", x => x.Id);
                    table.ForeignKey(
                        name: "FK_alkalmazottak_felhasznalok_FelhasznaloId",
                        column: x => x.FelhasznaloId,
                        principalTable: "felhasznalok",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "foglalasok",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FelhasznaloId = table.Column<int>(type: "int", nullable: false),
                    Idopont = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Letszam = table.Column<int>(type: "int", nullable: false),
                    Megjegyzes = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    Statusz = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    LetrehozasIdopont = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TavozasIdopont = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    KezeloAlkalmazottId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_foglalasok", x => x.Id);
                    table.ForeignKey(
                        name: "FK_foglalasok_alkalmazottak_KezeloAlkalmazottId",
                        column: x => x.KezeloAlkalmazottId,
                        principalTable: "alkalmazottak",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_foglalasok_felhasznalok_FelhasznaloId",
                        column: x => x.FelhasznaloId,
                        principalTable: "felhasznalok",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "kiadasok",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Datum = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Osszeg = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    Leiras = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true),
                    KategoriaId = table.Column<int>(type: "int", nullable: false),
                    RogzitetteAlkalmazottId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kiadasok", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kiadasok_alkalmazottak_RogzitetteAlkalmazottId",
                        column: x => x.RogzitetteAlkalmazottId,
                        principalTable: "alkalmazottak",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_kiadasok_kiadas_kategoriak_KategoriaId",
                        column: x => x.KategoriaId,
                        principalTable: "kiadas_kategoriak",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "foglalas_asztalok",
                columns: table => new
                {
                    FoglalasId = table.Column<int>(type: "int", nullable: false),
                    AsztalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_foglalas_asztalok", x => new { x.FoglalasId, x.AsztalId });
                    table.ForeignKey(
                        name: "FK_foglalas_asztalok_asztalok_AsztalId",
                        column: x => x.AsztalId,
                        principalTable: "asztalok",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_foglalas_asztalok_foglalasok_FoglalasId",
                        column: x => x.FoglalasId,
                        principalTable: "foglalasok",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "asztalok",
                columns: new[] { "Id", "Ferohely", "Szam" },
                values: new object[,]
                {
                    { 1, 2, 1 },
                    { 2, 2, 2 },
                    { 3, 4, 3 },
                    { 4, 4, 4 },
                    { 5, 6, 5 }
                });

            migrationBuilder.InsertData(
                table: "kiadas_kategoriak",
                columns: new[] { "Id", "Nev" },
                values: new object[,]
                {
                    { 1, "Alapanyag" },
                    { 2, "Rezsi" },
                    { 3, "Bér" },
                    { 4, "Karbantartás" },
                    { 5, "Egyéb" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_alkalmazottak_FelhasznaloId",
                table: "alkalmazottak",
                column: "FelhasznaloId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_asztalok_Szam",
                table: "asztalok",
                column: "Szam",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_felhasznalok_Email",
                table: "felhasznalok",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_foglalas_asztalok_AsztalId",
                table: "foglalas_asztalok",
                column: "AsztalId");

            migrationBuilder.CreateIndex(
                name: "IX_foglalasok_FelhasznaloId",
                table: "foglalasok",
                column: "FelhasznaloId");

            migrationBuilder.CreateIndex(
                name: "IX_foglalasok_KezeloAlkalmazottId",
                table: "foglalasok",
                column: "KezeloAlkalmazottId");

            migrationBuilder.CreateIndex(
                name: "IX_kiadas_kategoriak_Nev",
                table: "kiadas_kategoriak",
                column: "Nev",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_kiadasok_KategoriaId",
                table: "kiadasok",
                column: "KategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_kiadasok_RogzitetteAlkalmazottId",
                table: "kiadasok",
                column: "RogzitetteAlkalmazottId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "foglalas_asztalok");

            migrationBuilder.DropTable(
                name: "kiadasok");

            migrationBuilder.DropTable(
                name: "asztalok");

            migrationBuilder.DropTable(
                name: "foglalasok");

            migrationBuilder.DropTable(
                name: "kiadas_kategoriak");

            migrationBuilder.DropTable(
                name: "alkalmazottak");

            migrationBuilder.DropTable(
                name: "felhasznalok");
        }
    }
}
