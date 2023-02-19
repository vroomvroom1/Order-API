using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OrderAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOrderType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_OrderTypeItems_OrderTypeId",
                table: "OrderItems");

            migrationBuilder.DropTable(
                name: "OrderTypeItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_OrderTypeId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "OrderTypeId",
                table: "OrderItems");

            migrationBuilder.AddColumn<string>(
                name: "OrderType",
                table: "OrderItems",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderType",
                table: "OrderItems");

            migrationBuilder.AddColumn<int>(
                name: "OrderTypeId",
                table: "OrderItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "OrderTypeItems",
                columns: table => new
                {
                    OrderTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTypeItems", x => x.OrderTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderTypeId",
                table: "OrderItems",
                column: "OrderTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_OrderTypeItems_OrderTypeId",
                table: "OrderItems",
                column: "OrderTypeId",
                principalTable: "OrderTypeItems",
                principalColumn: "OrderTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
