using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Magazine.Data.Migrations
{
    public partial class AddedSeedPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CategoryId", "Content", "CreatedDate" },
                values: new object[] { 1, 2, "Bla bla bla this is a content", new DateTime(2018, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
