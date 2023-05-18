using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreIdentity.Migrations
{
    public partial class AddMunicipiosEstadosPais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Municipio",
                table: "Cliente",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CEstados",
                columns: table => new
                {
                    EstadoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaveEstado = table.Column<int>(type: "int", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Abreviacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CEstados", x => x.EstadoId);
                });

            migrationBuilder.CreateTable(
                name: "CPais",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    c_Pais = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripción = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Formato_de_código_postal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Formato_de_Registro_de_Identidad_Tributaria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Validación_del_Registro_de_Identidad_Tributaria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Agrupaciones = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPais", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CMunicipios",
                columns: table => new
                {
                    MunicipioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstadoId = table.Column<int>(type: "int", nullable: false),
                    ClaveMunicipio = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CMunicipios", x => x.MunicipioId);
                    table.ForeignKey(
                        name: "FK_CMunicipios_CEstados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "CEstados",
                        principalColumn: "EstadoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CMunicipios_EstadoId",
                table: "CMunicipios",
                column: "EstadoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CMunicipios");

            migrationBuilder.DropTable(
                name: "CPais");

            migrationBuilder.DropTable(
                name: "CEstados");

            migrationBuilder.DropColumn(
                name: "Municipio",
                table: "Cliente");
        }
    }
}
