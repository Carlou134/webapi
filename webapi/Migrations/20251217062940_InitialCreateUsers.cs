using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Rol",
                schema: "dbo",
                columns: table => new
                {
                    IdRol = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombreRol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                schema: "dbo",
                columns: table => new
                {
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.UsuarioId);
                    table.ForeignKey(
                        name: "FK_Usuario_Rol_RolId",
                        column: x => x.RolId,
                        principalSchema: "dbo",
                        principalTable: "Rol",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Rol",
                columns: new[] { "IdRol", "NombreRol" },
                values: new object[,]
                {
                    { new Guid("240acf13-8d8c-433d-90e0-f9c8b1dea10c"), "ADMIN" },
                    { new Guid("240acf13-8d8c-433d-90e0-f9c8b1dea11c"), "USER" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Usuario",
                columns: new[] { "UsuarioId", "Email", "NombreUsuario", "Password", "RolId" },
                values: new object[] { new Guid("240acf13-8d8c-433d-90e0-f9c8b1dea20c"), "Prueba@Gmail.com", "Administrador", "$2a$12$lBS.LD7m9wNn8eAtt9LZl.y5Fe/GqTqWrwSEnhiBNhr.DTBGqpt2C", new Guid("240acf13-8d8c-433d-90e0-f9c8b1dea10c") });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_RolId",
                schema: "dbo",
                table: "Usuario",
                column: "RolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Rol",
                schema: "dbo");
        }
    }
}
