using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class SeedYears : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "StandingsYears",
                columns: new[] { "StandingsYearId", "Year" },
                values: new object[,]
                {
                    { 1950, 1950 },
                    { 1951, 1951 },
                    { 1952, 1952 },
                    { 1953, 1953 },
                    { 1954, 1954 },
                    { 1955, 1955 },
                    { 1956, 1956 },
                    { 1957, 1957 },
                    { 1958, 1958 },
                    { 1959, 1959 },
                    { 1960, 1960 },
                    { 1961, 1961 },
                    { 1962, 1962 },
                    { 1963, 1963 },
                    { 1964, 1964 },
                    { 1965, 1965 },
                    { 1966, 1966 },
                    { 1967, 1967 },
                    { 1968, 1968 },
                    { 1969, 1969 },
                    { 1970, 1970 },
                    { 1971, 1971 },
                    { 1972, 1972 },
                    { 1973, 1973 },
                    { 1974, 1974 },
                    { 1975, 1975 },
                    { 1976, 1976 },
                    { 1977, 1977 },
                    { 1978, 1978 },
                    { 1979, 1979 },
                    { 1980, 1980 },
                    { 1981, 1981 },
                    { 1982, 1982 },
                    { 1983, 1983 },
                    { 1984, 1984 },
                    { 1985, 1985 },
                    { 1986, 1986 },
                    { 1987, 1987 },
                    { 1988, 1988 },
                    { 1989, 1989 },
                    { 1990, 1990 },
                    { 1991, 1991 },
                    { 1992, 1992 },
                    { 1993, 1993 },
                    { 1994, 1994 },
                    { 1995, 1995 },
                    { 1996, 1996 },
                    { 1997, 1997 },
                    { 1998, 1998 },
                    { 1999, 1999 },
                    { 2000, 2000 },
                    { 2001, 2001 },
                    { 2002, 2002 },
                    { 2003, 2003 },
                    { 2004, 2004 },
                    { 2005, 2005 },
                    { 2006, 2006 },
                    { 2007, 2007 },
                    { 2008, 2008 },
                    { 2009, 2009 },
                    { 2010, 2010 },
                    { 2011, 2011 },
                    { 2012, 2012 },
                    { 2013, 2013 },
                    { 2014, 2014 },
                    { 2015, 2015 },
                    { 2016, 2016 },
                    { 2017, 2017 },
                    { 2018, 2018 },
                    { 2019, 2019 },
                    { 2020, 2020 },
                    { 2021, 2021 },
                    { 2022, 2022 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1950);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1951);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1952);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1953);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1954);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1955);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1956);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1957);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1958);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1959);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1960);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1961);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1962);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1963);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1964);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1965);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1966);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1967);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1968);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1969);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1970);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1971);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1972);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1973);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1974);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1975);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1976);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1977);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1978);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1979);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1980);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1981);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1982);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1983);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1984);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1985);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1986);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1987);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1988);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1989);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1990);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1991);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1992);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1993);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1994);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1995);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1996);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1997);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1998);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 1999);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 2000);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 2001);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 2002);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 2003);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 2004);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 2005);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 2006);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 2007);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 2008);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 2009);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 2010);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 2011);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 2012);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 2013);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 2014);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 2015);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 2016);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 2017);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 2018);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 2019);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 2020);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 2021);

            migrationBuilder.DeleteData(
                table: "StandingsYears",
                keyColumn: "StandingsYearId",
                keyValue: 2022);
        }
    }
}
