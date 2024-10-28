using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thunders.Tecnologia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Peoples",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peoples", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Peoples",
                columns: new[] { "Id", "DateOfBirth", "Email", "Name" },
                values: new object[] { new Guid("b76aeea0-5ddf-4f21-8dbd-5a8a18c7f9d0"), new DateTime(1987, 10, 29, 0, 0, 0, 0, DateTimeKind.Utc), "pedro@viscarditecnologia.com.br", "Pedro Paulo Orasmo Viscardi" });

            migrationBuilder.CreateIndex(
                name: "IX_Peoples_Email",
                table: "Peoples",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Peoples");
        }
    }
}
