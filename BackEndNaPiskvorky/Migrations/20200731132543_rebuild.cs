using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEndNaPiskvorky.Migrations
{
    public partial class rebuild : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Album",
                table: "Song");

            migrationBuilder.DropColumn(
                name: "Artist",
                table: "Song");

            migrationBuilder.DropColumn(
                name: "LinkToImage",
                table: "Song");

            migrationBuilder.DropColumn(
                name: "NumberOfAlbums",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "NumberOfSongs",
                table: "Artist");

            migrationBuilder.AddColumn<int>(
                name: "AlbumId",
                table: "Song",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Length",
                table: "Song",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Song_AlbumId",
                table: "Song",
                column: "AlbumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Song_Album_AlbumId",
                table: "Song",
                column: "AlbumId",
                principalTable: "Album",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Song_Album_AlbumId",
                table: "Song");

            migrationBuilder.DropIndex(
                name: "IX_Song_AlbumId",
                table: "Song");

            migrationBuilder.DropColumn(
                name: "AlbumId",
                table: "Song");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "Song");

            migrationBuilder.AddColumn<string>(
                name: "Album",
                table: "Song",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Artist",
                table: "Song",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LinkToImage",
                table: "Song",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfAlbums",
                table: "Artist",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfSongs",
                table: "Artist",
                nullable: false,
                defaultValue: 0);
        }
    }
}
