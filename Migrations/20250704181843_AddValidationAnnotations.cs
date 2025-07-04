using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicMVCApp.Migrations
{
    /// <inheritdoc />
    public partial class AddValidationAnnotations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Patients",
                newName: "Suburb");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Patients",
                newName: "Province");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Patients",
                newName: "PostalCode");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Patients",
                newName: "DateInserted");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Patients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Patients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HomeAddress",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IDNumber",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfChildren",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Patients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateInserted",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Appointments",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "HomeAddress",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "IDNumber",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "NumberOfChildren",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "DateInserted",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "Suburb",
                table: "Patients",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "Province",
                table: "Patients",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "Patients",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "DateInserted",
                table: "Patients",
                newName: "DateOfBirth");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
