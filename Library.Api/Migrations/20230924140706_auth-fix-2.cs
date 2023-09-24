using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Api.Migrations
{
    /// <inheritdoc />
    public partial class authfix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Device_Accounts_AccountId",
                table: "Device");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Device",
                table: "Device");

            migrationBuilder.DropIndex(
                name: "IX_Device_AccountId",
                table: "Device");

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountId",
                table: "Device",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Device",
                table: "Device",
                columns: new[] { "AccountId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_Device_Accounts_AccountId",
                table: "Device",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Device_Accounts_AccountId",
                table: "Device");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Device",
                table: "Device");

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountId",
                table: "Device",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Device",
                table: "Device",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Device_AccountId",
                table: "Device",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Device_Accounts_AccountId",
                table: "Device",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
