using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MM_Kennels.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cages",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CageWeightMin = table.Column<int>(nullable: false),
                    CageWeightMax = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Weight = table.Column<int>(nullable: false),
                    StartDate = table.Column<int>(nullable: false),
                    LengthOfStay = table.Column<int>(nullable: false),
                    CageID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Animals_Cages_CageID",
                        column: x => x.CageID,
                        principalTable: "Cages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animals_CageID",
                table: "Animals",
                column: "CageID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropTable(
                name: "Cages");
        }
    }
}
