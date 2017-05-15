using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace app.api.Migrations
{
    public partial class Optionaltenant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Tenants_TenantID",
                table: "Properties");

            migrationBuilder.AlterColumn<int>(
                name: "TenantID",
                table: "Properties",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Tenants_TenantID",
                table: "Properties",
                column: "TenantID",
                principalTable: "Tenants",
                principalColumn: "TenantID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Tenants_TenantID",
                table: "Properties");

            migrationBuilder.AlterColumn<int>(
                name: "TenantID",
                table: "Properties",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Tenants_TenantID",
                table: "Properties",
                column: "TenantID",
                principalTable: "Tenants",
                principalColumn: "TenantID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
