using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoredProcedures.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
                    CREATE PROCEDURE sp_bestSellingStaff
                    AS
	                    DECLARE @name NVARCHAR(MAX), @count INT
	                    SELECT TOP 1 @name = p.Name, @count = COUNT(*) FROM Persons p
	                    JOIN Orders o
		                    ON p.PersonId = o.PersonId
	                    GROUP By p.Name
	                    ORDER By COUNT(*) DESC
	                    RETURN @count
                    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"DROP PROCEDURE sp_bestSellingStaff");
        }
    }
}
