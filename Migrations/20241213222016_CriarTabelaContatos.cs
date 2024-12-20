using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZapEnvioSeguro.Migrations
{
    /// <inheritdoc />
    public partial class CriarTabelaContatos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contatos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "O"),
                    IsBusiness = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PushName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateLastReceivedMsg = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastSentMsg = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastSentMsgId = table.Column<long>(type: "bigint", nullable: true),
                    IdEmpresa = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contatos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contatos");
        }
    }
}
