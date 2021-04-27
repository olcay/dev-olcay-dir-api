using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class PetSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "Id", "Age", "CityId", "CreatedById", "CreationDate", "Description", "FoundHome", "FromWhere", "Gender", "IsDeleted", "IsPublished", "Name", "PetType", "PublishDate", "RaceId", "Size", "Title" },
                values: new object[] { new Guid("102b566b-ba1f-404c-b2df-e2cde39ade09"), 1, 34, 1, new DateTimeOffset(new DateTime(2021, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), "Evde anne sütüyle büyüyen oyuncu", false, 3, 1, false, true, "Mişa", 1, new DateTimeOffset(new DateTime(2021, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 43, 1, "Norveç orman melezi bebek" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: new Guid("102b566b-ba1f-404c-b2df-e2cde39ade09"));
        }
    }
}
