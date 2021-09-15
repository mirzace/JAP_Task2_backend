using Microsoft.EntityFrameworkCore.Migrations;

namespace ScreenplayApp.Infrastructure.Migrations
{
    public partial class AddedStoredProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var spGetTop10RatedMovies = 
                @"CREATE PROCEDURE GetTop10RatedMovies
                AS
                BEGIN
                    SELECT TOP 10 Screenplays.Id, Screenplays.Title, COUNT(*) AS number_of_ratings FROM Screenplays
                    INNER JOIN Ratings
                    ON Screenplays.Id = Ratings.ScreenplayId
                    WHERE Screenplays.Category = 'Movie'
                    GROUP BY Screenplays.Id, Screenplays.Title
                    ORDER BY COUNT(*) DESC
                END";

            var spGetMostViewedMoviesForPeriod = 
                @"CREATE PROCEDURE GetMostViewedMoviesForPeriod @StartDate datetime2, @EndDate datetime2
                AS
                BEGIN
	                SELECT TOP 10 Screenplays.Id, Screenplays.Title, query.screening_number FROM Screenplays
	                INNER JOIN
	                (
	                SELECT qry.ScreenplayId, COUNT(qry.ScreenplayId) AS screening_number FROM (
	                SELECT Tickets.ScreenplayId, Tickets.Date, Tickets.Location FROM Tickets
	                WHERE Tickets.Date BETWEEN @StartDate AND @EndDate
	                GROUP BY Tickets.ScreenplayId, Tickets.Date, Tickets.Location) AS qry
	                GROUP BY qry.ScreenplayId
	                ) as query
	                ON Screenplays.Id = query.ScreenplayId
	                ORDER BY query.screening_number DESC
                END";

            var spGetMostSoldMoviesWithoutRating = 
                @"CREATE PROCEDURE GetMostSoldMoviesWithoutRating
                AS
                BEGIN
	                SELECT DISTINCT dbo.Screenplays.Id, Screenplays.Title, COUNT(Tickets.ScreenplayId) AS tickets_sold, Tickets.Location, Tickets.Date
	                FROM Screenplays
	                LEFT OUTER JOIN Ratings
	                ON Screenplays.Id = Ratings.ScreenplayId
	                RIGHT OUTER JOIN Tickets
	                ON Tickets.ScreenplayId = Screenplays.Id
	                WHERE Ratings.Rate IS NULL AND Screenplays.Category = 'Movie' AND Tickets.BookingId IS NOT NULL
	                GROUP BY Screenplays.Id,  Screenplays.Title, Tickets.Location, Tickets.Date
	                ORDER BY COUNT(Tickets.ScreenplayId) DESC
                END";

            migrationBuilder.Sql(spGetTop10RatedMovies);
            migrationBuilder.Sql(spGetMostViewedMoviesForPeriod);
            migrationBuilder.Sql(spGetMostSoldMoviesWithoutRating);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var spGetTop10RatedMovies = @"DROP PROCEDURE GetTop10RatedMovies";
            var spGetMostViewedMoviesForPeriod = @"DROP PROCEDURE GetMostViewedMoviesForPeriod";
            var spGetMostSoldMoviesWithoutRating = @"DROP PROCEDURE GetMostSoldMoviesWithoutRating";

            migrationBuilder.Sql(spGetTop10RatedMovies);
            migrationBuilder.Sql(spGetMostViewedMoviesForPeriod);
            migrationBuilder.Sql(spGetMostSoldMoviesWithoutRating);
        }
    }
}
