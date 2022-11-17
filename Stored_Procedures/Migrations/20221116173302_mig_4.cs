using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoredProcedures.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
                    CREATE PROCEDURE sp_PersonOrders2
                    (
	                    @PersonId INT,
	                    @Name NVARCHAR(MAX) OUTPUT
                    )
                    AS
                    SELECT @Name = p.Name FROM Persons p
                    JOIN Orders o
                        ON p.PersonId = o.PersonId
                    WHERE p.PersonId = @PersonId
                    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DROP PROC sp_PersonOrders2");
        }
    }
}
