﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.G03.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeDepartmentRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "WorkForId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_WorkForId",
                table: "Employees",
                column: "WorkForId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Department_WorkForId",
                table: "Employees",
                column: "WorkForId",
                principalTable: "Department",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Department_WorkForId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_WorkForId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "WorkForId",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
