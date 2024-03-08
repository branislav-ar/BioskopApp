using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class V5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bioskop",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: true),
                    BrojSala = table.Column<int>(type: "int", nullable: false),
                    BrojMestaUSalama = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bioskop", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Sediste",
                columns: table => new
                {
                    Broj = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Zauzetost = table.Column<bool>(type: "bit", nullable: false),
                    FormaBioskopaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sediste", x => x.Broj);
                });

            migrationBuilder.CreateTable(
                name: "FormaBioskopa",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormaBioskopa", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FormaBioskopa_Bioskop_ID",
                        column: x => x.ID,
                        principalTable: "Bioskop",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Film",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CenaKarte = table.Column<int>(type: "int", nullable: false),
                    FormaBioskopaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Film", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Film_FormaBioskopa_FormaBioskopaID",
                        column: x => x.FormaBioskopaID,
                        principalTable: "FormaBioskopa",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sala",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Broj = table.Column<int>(type: "int", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    formaBioskopaID = table.Column<int>(type: "int", nullable: true),
                    bioskopid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sala", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sala_Bioskop_bioskopid",
                        column: x => x.bioskopid,
                        principalTable: "Bioskop",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sala_FormaBioskopa_formaBioskopaID",
                        column: x => x.formaBioskopaID,
                        principalTable: "FormaBioskopa",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalaFilm",
                columns: table => new
                {
                    FilmID = table.Column<int>(type: "int", nullable: false),
                    SalaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaFilm", x => new { x.SalaID, x.FilmID });
                    table.ForeignKey(
                        name: "FK_SalaFilm_Film_FilmID",
                        column: x => x.FilmID,
                        principalTable: "Film",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalaFilm_Sala_SalaID",
                        column: x => x.SalaID,
                        principalTable: "Sala",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalaSediste",
                columns: table => new
                {
                    SedisteID = table.Column<int>(type: "int", nullable: false),
                    SalaID = table.Column<int>(type: "int", nullable: false),
                    BrojSedista = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaSediste", x => new { x.SalaID, x.SedisteID });
                    table.ForeignKey(
                        name: "FK_SalaSediste_Sala_SalaID",
                        column: x => x.SalaID,
                        principalTable: "Sala",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalaSediste_Sediste_SedisteID",
                        column: x => x.SedisteID,
                        principalTable: "Sediste",
                        principalColumn: "Broj",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Film_FormaBioskopaID",
                table: "Film",
                column: "FormaBioskopaID");

            migrationBuilder.CreateIndex(
                name: "IX_Sala_bioskopid",
                table: "Sala",
                column: "bioskopid");

            migrationBuilder.CreateIndex(
                name: "IX_Sala_formaBioskopaID",
                table: "Sala",
                column: "formaBioskopaID");

            migrationBuilder.CreateIndex(
                name: "IX_SalaFilm_FilmID",
                table: "SalaFilm",
                column: "FilmID");

            migrationBuilder.CreateIndex(
                name: "IX_SalaSediste_SedisteID",
                table: "SalaSediste",
                column: "SedisteID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalaFilm");

            migrationBuilder.DropTable(
                name: "SalaSediste");

            migrationBuilder.DropTable(
                name: "Film");

            migrationBuilder.DropTable(
                name: "Sala");

            migrationBuilder.DropTable(
                name: "Sediste");

            migrationBuilder.DropTable(
                name: "FormaBioskopa");

            migrationBuilder.DropTable(
                name: "Bioskop");
        }
    }
}
