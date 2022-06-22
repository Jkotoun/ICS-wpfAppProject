using Microsoft.EntityFrameworkCore.Migrations;

namespace Festival.DAL.Migrations
{
    public partial class odevzdani1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Bands_BandGuid",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Stages_StageGuid",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "Guid",
                table: "Stages",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "StageGuid",
                table: "Events",
                newName: "StageId");

            migrationBuilder.RenameColumn(
                name: "BandGuid",
                table: "Events",
                newName: "BandId");

            migrationBuilder.RenameColumn(
                name: "Guid",
                table: "Events",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Events_StageGuid",
                table: "Events",
                newName: "IX_Events_StageId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_BandGuid",
                table: "Events",
                newName: "IX_Events_BandId");

            migrationBuilder.RenameColumn(
                name: "Guid",
                table: "Bands",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Bands_BandId",
                table: "Events",
                column: "BandId",
                principalTable: "Bands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Stages_StageId",
                table: "Events",
                column: "StageId",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Bands_BandId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Stages_StageId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Stages",
                newName: "Guid");

            migrationBuilder.RenameColumn(
                name: "StageId",
                table: "Events",
                newName: "StageGuid");

            migrationBuilder.RenameColumn(
                name: "BandId",
                table: "Events",
                newName: "BandGuid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Events",
                newName: "Guid");

            migrationBuilder.RenameIndex(
                name: "IX_Events_StageId",
                table: "Events",
                newName: "IX_Events_StageGuid");

            migrationBuilder.RenameIndex(
                name: "IX_Events_BandId",
                table: "Events",
                newName: "IX_Events_BandGuid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Bands",
                newName: "Guid");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Bands_BandGuid",
                table: "Events",
                column: "BandGuid",
                principalTable: "Bands",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Stages_StageGuid",
                table: "Events",
                column: "StageGuid",
                principalTable: "Stages",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
