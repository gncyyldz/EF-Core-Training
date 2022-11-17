using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoredProcedures.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
                        CREATE PROCEDURE sp_PersonOrders
                        AS
	                        SELECT p.Name, COUNT(*) [Count] FROM Persons p
	                        JOIN Orders o
		                        ON p.PersonId = o.PersonId
	                        GROUP By p.Name
	                        ORDER By COUNT(*) DESC
                        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"DROP PROC sp_PersonOrders");
        }
    }
}
