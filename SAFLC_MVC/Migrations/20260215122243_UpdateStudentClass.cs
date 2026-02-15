using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAFLC_MVC.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStudentClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "tbl_Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "tbl_Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "tbl_Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "tbl_Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "tbl_Students",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "tbl_Students");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "tbl_Students");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "tbl_Students");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "tbl_Students");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "tbl_Students");
        }
    }
}
