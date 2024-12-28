using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tschiraplusAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserTaskAssignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "UserTaskAssignments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "UserTaskAssignments");
        }
    }
}
