using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class changeTrailerName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable("Trailers", "dbo","Trailer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
