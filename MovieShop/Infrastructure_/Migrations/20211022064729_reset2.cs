using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Infrastructure.Migrations
{
    public partial class reset2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>("DateOfBirth", "User", nullable: true);
            migrationBuilder.AlterColumn<DateTime>("LockoutEndDate", "User", nullable: true);
            migrationBuilder.AlterColumn<DateTime>("LastLoginDateTime", "User", nullable: true);
            migrationBuilder.AlterColumn<bool>("isLocked", "User", nullable: true);
            migrationBuilder.AlterColumn<int>("AccessFailedCount", "User", nullable: true);
            //migrationBuilder.RenameTable("Trailers", "Trailer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
