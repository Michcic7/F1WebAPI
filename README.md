# Formula 1 Web API

A REST API for accessing the data scraped from the Formula 1 official website's standings tab. It includes drivers, teams and race results.

Built with ASP.NET Core 7, PostgreSQL and Docker. The project showcases my 1 year of learning back-end programming.

## Features

- Data ranging from 1950 to 2023
- Pagination with metadata
- Rate limiting of requests
- ProblemDetails responses
- JWT authentication and authorisation with refresh tokens
- Code-first database approach
- Multi-container (Linux) application

## Authentication & Authorization

To register a new user with a username and password using a JSON body:

```
  POST /auth/register
```

To login into an account using a JSON body:

```
  POST /auth/login
```

To refresh the access token using the body returned from the login endpoint:

```
  POST /auth/refresh
```

Tokens lifetime:

- Access token - 10 minutes
  
- Refresh token - 1 day
  

## How to use

With Docker Desktop installed and running, run `docker-compose up --build` inside the `F1WebAPI` folder using CLI of choice.

## Endpoints

To access any of the endpoints, a user is required to have a valid non-expired JWT access token and put it into the 'Authorization' header of the request.

The endpoints give access to 3 resources:

- Driver
  
- Team
  
- Circuit
  

### Get all drivers

```
  GET /drivers
```

| Parameter | Type | Description |
| --- | --- | --- |
| `name` | `string` | Name filter |
| `page` | `int` | Page number ( > 0) |
| `pagesize` | `int` | The number of elements per returned page (1 - 40) |
| `nationality` | string | The three-letter code to filter drivers |

> /drivers?name=john&page=1&pagesize=20

```
{
    "totalDrivers": 28,
    "totalPages": 2,
    "currentPage": 1,
    "pageSize": 20,
    "nameFilter": "john",
    "nationalityFilter": null,
    "drivers": [
        {
            "driverId": 12,
            "name": "Johnny Claes",
            "nationality": null
        }, ...
```

### Get a driver by the ID

```
  GET /drivers/{id}
```

> /drivers/697

```
{
    "driverId": 697,
    "name": "Felipe Massa",
    "nationality": "BRA"
}
```

### Get the Drivers' Championship standing for the given year

```
  GET /drivers/driverstanding
```

| Parameter | Type | Description |
| --- | --- | --- |
| `year` | `int` | The year of the championship (1950 - 2022) |

> /drivers/driverstanding?year=2021

```
[
    {
        "position": 1,
        "driverName": "Max Verstappen",
        "nationality": "NED",
        "teamName": "Red Bull Racing Honda",
        "points": 395.5,
        "year": 2021
    }, ...
```

### Get all driver's championship standings

```
  GET /drivers/{id}/driverstandings
```

> /drivers/767/driverstandings

```
[
    {
        "position": 21,
        "driverName": "Pierre Gasly",
        "nationality": "FRA",
        "teamName": "Toro Rosso",
        "points": 0,
        "year": 2017
    }, ...
```

### Get all driver's race results from the given year

```
  GET /drivers/{id}/raceresults
```

| Parameter | Type | Description |
| --- | --- | --- |
| `year` | `int` | The year of the races (1950 - 2022) |

> /drivers/722/raceresults?year=2010

```
[
    {
        "position": 3,
        "circuitName": "Bahrain International Circuit",
        "date": "2010-03-14",
        "driverName": "Lewis Hamilton",
        "teamName": "McLaren Mercedes",
        "laps": 49,
        "time": "+23.182s",
        "points": 15
    }, ...
```

### Get all teams

```
  GET /teams
```

| Parameter | Type | Description |
| --- | --- | --- |
| `name` | `string` | Name filter |
| `page` | `int` | Page number ( > 0) |
| `pagesize` | `int` | The number of elements per returned page (1 - 40) |

> /teams?name=ferrari&page=3&pagesize=5

```
{
    "totalTeams": 17,
    "totalPages": 4,
    "currentPage": 3,
    "pageSize": 5,
    "nameFilter": "ferrari",
    "teams": [
        {
            "teamId": 294,
            "name": "Force India Ferrari"
        }, ...
```

### Get a team by the ID

```
  GET /teams/{id}
```

> /teams/320

```
{
    "teamId": 320,
    "name": "AlphaTauri Honda"
}
```

### Get the Constructors' Championship standing for the given year

```
  GET /teams/teamstanding
```

| Parameter | Type | Description |
| --- | --- | --- |
| `year` | `int` | The year of the championship (1958 - 2022) |

> /teams/teamstanding?year=1993

```
[
    {
        "position": 1,
        "name": "Williams Renault",
        "points": 168,
        "year": 1993
    }, ...
```

### Get all team's championship standings

```
  GET /teams/{id}/teamstandings
```

> /teams/298/teamstandings

```
[
    {
        "position": 4,
        "name": "Mercedes",
        "points": 214,
        "year": 2010
    }, ...
```

### Get all team's race results from the given year

```
  GET /teams/{id}/raceresults
```

| Parameter | Type | Description |
| --- | --- | --- |
| `year` | `int` | The year of the races (1950 - 2022) |

> /teams/325/raceresults?year=2022

```
[
    {
        "position": 12,
        "circuitName": "Bahrain International Circuit",
        "date": "2022-03-20",
        "driverName": "Lance Stroll",
        "teamName": "Aston Martin Aramco Mercedes",
        "laps": 57,
        "time": "+45.873s",
        "points": 0
    }, ...
```

### Get all circuits

```
  GET /circuits
```

| Parameter | Type | Description |
| --- | --- | --- |
| `name` | `string` | Name filter |
| `page` | `int` | Page number ( > 0) |
| `pagesize` | `int` | The number of elements per returned page (1 - 40) |

> /circuits?page=1&pageSize=20

```
{
  "totalCircuits": 115,
  "totalPages": 6,
  "currentPage": 1,
  "pageSize": 20,
  "nameFilter": null,
  "circuits": [
    {
      "circuitId": 1,
      "name": "Silverstone Circuit",
      "location": "Great Britain"
    }, ...
```

### Get a circuit by the ID

```
  GET /circuits/{id}
```

> /circuits/8

```
{
  "circuitId": 8,
  "name": "Nürburgring",
  "location": "Germany"
}
```

### Get race results from the given circuit from the given year

```
  GET /circuits/{id}/raceresults
```

| Parameter | Type | Description |
| --- | --- | --- |
| `year` | `int` | The year of the race (1950 - 2022) |

> /circuits/1/raceresults?year=1951

```
[
    {
        "position": 1,
        "circuitName": "Silverstone Circuit",
        "date": "1951-07-14",
        "driverName": "Jose Froilan Gonzalez",
        "teamName": "Ferrari",
        "laps": 90,
        "time": "2:42:18.200",
        "points": 8
    }, ...
```

## License

[MIT](https://choosealicense.com/licenses/mit/)
