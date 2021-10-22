using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class WholePatternsII : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trailers_Movie_MovieId",
                table: "Trailers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trailers",
                table: "Trailers");

            migrationBuilder.RenameTable(
                name: "Trailers",
                newName: "Trailer");

            migrationBuilder.RenameIndex(
                name: "IX_Trailers_MovieId",
                table: "Trailer",
                newName: "IX_Trailer_MovieId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Trailer",
                type: "nvarchar(2084)",
                maxLength: 2084,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trailer",
                table: "Trailer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trailer_Movie_MovieId",
                table: "Trailer",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trailer_Movie_MovieId",
                table: "Trailer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trailer",
                table: "Trailer");

            migrationBuilder.RenameTable(
                name: "Trailer",
                newName: "Trailers");

            migrationBuilder.RenameIndex(
                name: "IX_Trailer_MovieId",
                table: "Trailers",
                newName: "IX_Trailers_MovieId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Trailers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2084)",
                oldMaxLength: 2084,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trailers",
                table: "Trailers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trailers_Movie_MovieId",
                table: "Trailers",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
