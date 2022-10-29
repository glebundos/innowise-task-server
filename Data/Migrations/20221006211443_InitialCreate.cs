using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace innowise_task_server.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FridgeModels",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    year = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FridgeModels", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultQuantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Fridges",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModelID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fridges", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Fridges_FridgeModels_ModelID",
                        column: x => x.ModelID,
                        principalTable: "FridgeModels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FridgeProducts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FridgeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FridgeProducts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FridgeProducts_Fridges_FridgeID",
                        column: x => x.FridgeID,
                        principalTable: "Fridges",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FridgeProducts_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FridgeProducts_FridgeID",
                table: "FridgeProducts",
                column: "FridgeID");

            migrationBuilder.CreateIndex(
                name: "IX_FridgeProducts_ProductID",
                table: "FridgeProducts",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Fridges_ModelID",
                table: "Fridges",
                column: "ModelID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FridgeProducts");

            migrationBuilder.DropTable(
                name: "Fridges");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "FridgeModels");
        }
    }
}
