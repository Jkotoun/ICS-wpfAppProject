using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Festival.DAL.Migrations
{
    public partial class seedsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Bands",
                columns: new[] { "Id", "Country", "Description", "Genre", "ImgUrl", "Name", "ShortDescription" },
                values: new object[] { new Guid("873634ea-31a3-4fe1-adb9-fecb9be923d4"), null, "Mega super banda", null, "https://upload.wikimedia.org/wikipedia/commons/thumb/0/07/Metallica_at_The_O2_Arena_London_2008.jpg/1200px-Metallica_at_The_O2_Arena_London_2008.jpg", "Banda 1", null });

            migrationBuilder.InsertData(
                table: "Bands",
                columns: new[] { "Id", "Country", "Description", "Genre", "ImgUrl", "Name", "ShortDescription" },
                values: new object[] { new Guid("8d898b36-d7d3-46a4-b979-a14ef997136b"), null, "Fuj Fuj banda", null, "https://scontent-prg1-1.xx.fbcdn.net/v/t1.6435-0/p526x296/45428212_322097681904122_8124368898846883840_n.jpg?_nc_cat=108&ccb=1-3&_nc_sid=8bfeb9&_nc_ohc=R2ImRaXPTQEAX_Avgv4&_nc_ht=scontent-prg1-1.xx&tp=6&oh=52dbae9db02f0817f70e76e26a65bc41&oe=60AF53FD", "Banda 2", null });

            migrationBuilder.InsertData(
                table: "Stages",
                columns: new[] { "Id", "Description", "ImgUrl", "Name" },
                values: new object[] { new Guid("8e1212d4-c0b2-40c4-856f-18ac69b92226"), "Stage s výhledem", "https://c8.alamy.com/comp/BWHHRB/view-from-the-top-of-the-hill-at-the-top-of-the-park-stage-at-glastonbury-BWHHRB.jpg", "Stage na kopcu" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "BandId", "EndTime", "StageId", "StartTime" },
                values: new object[] { new Guid("ab1a03eb-172a-4690-b3db-55361959c309"), new Guid("873634ea-31a3-4fe1-adb9-fecb9be923d4"), new DateTime(2015, 10, 5, 15, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8e1212d4-c0b2-40c4-856f-18ac69b92226"), new DateTime(2015, 10, 5, 16, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "BandId", "EndTime", "StageId", "StartTime" },
                values: new object[] { new Guid("426820af-3a66-48c2-b8d6-00ef2d5ac328"), new Guid("8d898b36-d7d3-46a4-b979-a14ef997136b"), new DateTime(2015, 8, 5, 7, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8e1212d4-c0b2-40c4-856f-18ac69b92226"), new DateTime(2015, 5, 5, 8, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("426820af-3a66-48c2-b8d6-00ef2d5ac328"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("ab1a03eb-172a-4690-b3db-55361959c309"));

            migrationBuilder.DeleteData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: new Guid("873634ea-31a3-4fe1-adb9-fecb9be923d4"));

            migrationBuilder.DeleteData(
                table: "Bands",
                keyColumn: "Id",
                keyValue: new Guid("8d898b36-d7d3-46a4-b979-a14ef997136b"));

            migrationBuilder.DeleteData(
                table: "Stages",
                keyColumn: "Id",
                keyValue: new Guid("8e1212d4-c0b2-40c4-856f-18ac69b92226"));
        }
    }
}
