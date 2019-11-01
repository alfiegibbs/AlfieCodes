using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlfieCodes.Migrations
{
    public partial class AddingCompositeKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogPostTags",
                table: "BlogPostTags");

            migrationBuilder.DropIndex(
                name: "IX_BlogPostTags_BlogPostId",
                table: "BlogPostTags");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BlogPostTags");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogPostTags",
                table: "BlogPostTags",
                columns: new[] { "BlogPostId", "TagId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogPostTags",
                table: "BlogPostTags");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "BlogPostTags",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogPostTags",
                table: "BlogPostTags",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostTags_BlogPostId",
                table: "BlogPostTags",
                column: "BlogPostId");
        }
    }
}
