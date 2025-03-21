using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddedTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FromAccountId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ToAccountId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FromAccountId",
                table: "Transactions",
                column: "FromAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ToAccountId",
                table: "Transactions",
                column: "ToAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_FromAccountId",
                table: "Transactions",
                column: "FromAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_ToAccountId",
                table: "Transactions",
                column: "ToAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_FromAccountId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_ToAccountId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_FromAccountId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_ToAccountId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "FromAccountId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ToAccountId",
                table: "Transactions");
        }
    }
}
