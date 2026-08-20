using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ClientHub.Migrations
{
    /// <inheritdoc />
    public partial class AddClientOwnerAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserId",
                table: "Clients",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "PostalCodes",
                columns: new[] { "Id", "City", "Code" },
                values: new object[,]
                {
                    { 1, "Lisboa", "1000-001" },
                    { 2, "Porto", "4000-100" },
                    { 3, "Coimbra", "3000-000" },
                    { 4, "Braga", "4700-000" },
                    { 5, "Faro", "8000-000" },
                    { 6, "Aveiro", "3800-000" },
                    { 7, "Évora", "7000-000" },
                    { 8, "Setúbal", "2900-000" },
                    { 9, "Funchal", "9000-000" },
                    { 10, "Ponta Delgada", "9500-000" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "PasswordHash" },
                values: new object[] { new Guid("11111111-2222-3333-4444-555555555555"), new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "demo@clienthub.dev", "AQAAAAIAAYagAAAAEDurHBiNUlg+IflHWRfQNTbsgriWYFY65X9z6wjkLpc7jTv+nqk4yZTtATQR6XNdVA==" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Address", "CreatedAt", "CreatedByUserId", "Email", "FirstName", "LastName", "Phone", "PostalCodeId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Rua Augusta, 100", new DateTime(2026, 8, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-2222-3333-4444-555555555555"), "john.smith@example.com", "John", "Smith", "+351 912 345 001", 1, null },
                    { 2, "Avenida da Liberdade, 200", new DateTime(2026, 8, 14, 14, 30, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-2222-3333-4444-555555555555"), "maria.silva@example.com", "Maria", "Silva", "+351 965 432 109", 2, null },
                    { 3, "Rua Ferreira Borges, 300", new DateTime(2026, 8, 12, 11, 15, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-2222-3333-4444-555555555555"), "pedro.santos@example.com", "Pedro", "Santos", "+351 961 234 567", 3, null },
                    { 4, "Praça do Município, 400", new DateTime(2026, 8, 10, 16, 45, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-2222-3333-4444-555555555555"), "ana.souza@example.com", "Ana", "Souza", "+351 917 654 321", 4, null },
                    { 5, null, new DateTime(2026, 8, 8, 10, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-2222-3333-4444-555555555555"), "carlos.oliveira@example.com", "Carlos", "Oliveira", "+351 969 876 543", 5, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CreatedByUserId",
                table: "Clients",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Users_CreatedByUserId",
                table: "Clients",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Users_CreatedByUserId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_CreatedByUserId",
                table: "Clients");

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555555"));

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Clients");
        }
    }
}
