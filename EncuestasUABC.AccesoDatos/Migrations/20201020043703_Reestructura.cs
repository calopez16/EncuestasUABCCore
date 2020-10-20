using Microsoft.EntityFrameworkCore.Migrations;

namespace EncuestasUABC.AccesoDatos.Migrations
{
    public partial class Reestructura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_RolesSistema_RolId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "RolesSistema");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RolId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RolId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RolId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RolesSistema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesSistema", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RolId",
                table: "AspNetUsers",
                column: "RolId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_RolesSistema_RolId",
                table: "AspNetUsers",
                column: "RolId",
                principalTable: "RolesSistema",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
