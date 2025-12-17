using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace webapi.Migrations.Tareas
{
    /// <inheritdoc />
    public partial class InitialCreateTareas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    CategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Peso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "Tarea",
                columns: table => new
                {
                    TareaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prioridad = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Puntos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarea", x => x.TareaId);
                    table.ForeignKey(
                        name: "FK_Tarea_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "CategoriaId", "Descripcion", "Nombre", "Peso" },
                values: new object[,]
                {
                    { new Guid("240acf13-8d8c-433d-90e0-f9c8b1dea04b"), "", "Actividades pendientes", 20 },
                    { new Guid("240acf13-8d8c-433d-90e0-f9c8b1dea05b"), "", "Actividades personales", 50 }
                });

            migrationBuilder.InsertData(
                table: "Tarea",
                columns: new[] { "TareaId", "CategoriaId", "Descripcion", "FechaCreacion", "Prioridad", "Puntos", "Titulo" },
                values: new object[,]
                {
                    { new Guid("240acf13-8d8c-433d-90e0-f9c8b1dea04c"), new Guid("240acf13-8d8c-433d-90e0-f9c8b1dea04b"), "", new DateTime(2025, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0, "Pago de servicios públicos" },
                    { new Guid("240acf13-8d8c-433d-90e0-f9c8b1dea05c"), new Guid("240acf13-8d8c-433d-90e0-f9c8b1dea05b"), "", new DateTime(2025, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0, "Terminar de ver pelicula en Netflix" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tarea_CategoriaId",
                table: "Tarea",
                column: "CategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tarea");

            migrationBuilder.DropTable(
                name: "Categoria");
        }
    }
}
