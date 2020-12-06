using Microsoft.EntityFrameworkCore.Migrations;

namespace Festify.Migrations
{
    public partial class AddedSessionTagTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SessionTag",
                columns: table => new
                {
                    SessionId = table.Column<int>(nullable: false),
                    Tag = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionTag", x => new { x.SessionId, x.Tag });
                    table.ForeignKey(
                        name: "FK_SessionTag_Session_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Session",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SessionTag");
        }
    }
}
