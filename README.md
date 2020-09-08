# Bookmarks

This is an api that will return articles linked with a bookmark.

## Supported endpoints

- [x] GET /bookmarks
- [x] GET /bookmarks/id
- [x] POST /bookmarks
- [] DELETE /bookmarks/id

## Todo
- [x] Handle GET request where the bookmark doesn't exist
- [] GET all bookmarks shouldn't return articles in json
- [] Handle POST request where the bookmark already exists
- [] Integration test with mock repositories 
- [] Acceptance test with second dockerised database
- [] Add logger
- [] [Option pattern](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-3.1) vs [Configuration](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-3.1)
- [] [Service lifetime](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1#service-lifetimes)
- [x] Use Dapper to connect to a dockerised PostgresDB

## Learnings

1. How to do integration tests in .NET Core using [WebApplicationFactory](https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-3.1)

## Database
This project uses a containerised Postgres database. The infrastructure for it is defined in /Database

Steps to run the database:

1. Build the docker image

```
docker build -t bookmarks-db .
```

2. Run the container in a detached mode with a postgres password (this will automatically belong to the `postgres` user) and portforward to localhost 5432

```
docker run -e POSTGRES_PASSWORD=mysecretpassword -p 5432:5432 -d bookmarks-db 
```

3. Exec into the container database and automatically connect to bookmarks-db:

```
docker exec -u postgres -it <container-id> psql --dbname=bookmarks-db
```