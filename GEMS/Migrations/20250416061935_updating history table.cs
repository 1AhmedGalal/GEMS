using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GEMS.Migrations
{
    public partial class updatinghistorytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeightUsed",
                table: "UserExercises");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "UserExercises",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "UserExercises");

            migrationBuilder.AddColumn<float>(
                name: "WeightUsed",
                table: "UserExercises",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
