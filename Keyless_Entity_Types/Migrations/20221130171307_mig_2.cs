using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeylessEntityTypes.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
                    CREATE VIEW vw_PersonOrderCount
                    AS
	                    SELECT p.Name, COUNT(*) Count FROM Persons p
	                    JOIN Orders o 
		                    ON p.PersonId = o.PersonId
	                    GROUP By p.Name
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"DROP VIEW vw_PersonOrderCount");
        }
    }
}
