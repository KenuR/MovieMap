This solution consists of the following:

1. GetAndStoreMovieDataHttpFunc azure function (HTTP trigger) accepts requests with a movie name. 

2. TvMazeApiService calls TV maze public API and retrieves information about the movie.

3. Movie name, genres/tags and rating is saved in SQL server.

4. GetHighestRatedPerGenreHttpFunc can be called, accepts request with "genre" name as a parameter. Returns highest rated movie
within that genre.

Prequisites:

Local instance of SQL Server should be installed and "MovieMap" database created.

You can trigger the functions from powershell:

Invoke-RestMethod -Uri http://localhost:7071/api/GetAndStoreMovieDataHttpFunc -ContentType application/json -Body '{"movieName": "Some Movie Name"}' -Method POST

Invoke-RestMethod -Uri http://localhost:7071/api/genres/thriller -Method GET

TO DO:

No unit tests because there is nothing to test.
Can be deployed to azure.