using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class RefactorDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "FoundHome",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "PublishDate",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Accounts");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Revoked",
                table: "RefreshToken",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Expires",
                table: "RefreshToken",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Created",
                table: "RefreshToken",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Adopted",
                table: "Pets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdoptedById",
                table: "Pets",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Created",
                table: "Pets",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Deleted",
                table: "Pets",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Published",
                table: "Pets",
                nullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Verified",
                table: "Accounts",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Updated",
                table: "Accounts",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ResetTokenExpires",
                table: "Accounts",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "PasswordReset",
                table: "Accounts",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Created",
                table: "Accounts",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Banned",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "Accounts",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: new Guid("102b566b-ba1f-404c-b2df-e2cde39ade09"),
                columns: new[] { "Created", "Published" },
                values: new object[] { new DateTimeOffset(new DateTime(2021, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2021, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_Pets_AdoptedById",
                table: "Pets",
                column: "AdoptedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Accounts_AdoptedById",
                table: "Pets",
                column: "AdoptedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Accounts_AdoptedById",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_AdoptedById",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Adopted",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "AdoptedById",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Published",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Banned",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "Accounts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Revoked",
                table: "RefreshToken",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Expires",
                table: "RefreshToken",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "RefreshToken",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreationDate",
                table: "Pets",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "FoundHome",
                table: "Pets",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Pets",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Pets",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "PublishDate",
                table: "Pets",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Verified",
                table: "Accounts",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Accounts",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ResetTokenExpires",
                table: "Accounts",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PasswordReset",
                table: "Accounts",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Accounts",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Accounts",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: new Guid("102b566b-ba1f-404c-b2df-e2cde39ade09"),
                columns: new[] { "CreationDate", "IsPublished", "PublishDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2021, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), true, new DateTimeOffset(new DateTime(2021, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) });
        }
    }
}
