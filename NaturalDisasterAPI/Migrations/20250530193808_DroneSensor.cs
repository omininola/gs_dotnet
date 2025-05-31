using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NaturalDisasterAPI.Migrations
{
    /// <inheritdoc />
    public partial class DroneSensor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relatorios_Drones_DroneId",
                table: "Relatorios");

            migrationBuilder.DropForeignKey(
                name: "FK_Relatorios_Usuarios_UsuarioId",
                table: "Relatorios");

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioId",
                table: "Relatorios",
                type: "NUMBER(19)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "NUMBER(19)");

            migrationBuilder.AlterColumn<long>(
                name: "DroneId",
                table: "Relatorios",
                type: "NUMBER(19)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "NUMBER(19)");

            migrationBuilder.CreateTable(
                name: "Sensores",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DroneId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    Tipo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sensores_Drones_DroneId",
                        column: x => x.DroneId,
                        principalTable: "Drones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sensores_DroneId",
                table: "Sensores",
                column: "DroneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Relatorios_Drones_DroneId",
                table: "Relatorios",
                column: "DroneId",
                principalTable: "Drones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Relatorios_Usuarios_UsuarioId",
                table: "Relatorios",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relatorios_Drones_DroneId",
                table: "Relatorios");

            migrationBuilder.DropForeignKey(
                name: "FK_Relatorios_Usuarios_UsuarioId",
                table: "Relatorios");

            migrationBuilder.DropTable(
                name: "Sensores");

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioId",
                table: "Relatorios",
                type: "NUMBER(19)",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "NUMBER(19)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "DroneId",
                table: "Relatorios",
                type: "NUMBER(19)",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "NUMBER(19)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Relatorios_Drones_DroneId",
                table: "Relatorios",
                column: "DroneId",
                principalTable: "Drones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relatorios_Usuarios_UsuarioId",
                table: "Relatorios",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
