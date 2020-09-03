# Bookmarks

This is an api that will return articles linked with a bookmark.

## Supported endpoints

- [x] GET /bookmarks
- [x] GET /bookmarks/id
- [] POST /bookmarks
- [] DELETE /bookmarks/id

## Todo
- [] Make bookmark id required for POST using Validate Attribute
- [] Handle GET request where the bookmark doesn't exist
- [] Integration test with mock repositories 
- [] [Option pattern](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-3.1) vs [Configuration](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-3.1)
- [] [Service lifetime](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1#service-lifetimes)
- [] Use Dapper to connect to a dockerised PostgresDB

## Learnings

1. How to do integration tests in .NET Core using [WebApplicationFactory](https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-3.1)

## Database

I'm in the middle of setting up a Postgres database inside a container and connect the application to it. 

The code for the containerised databse is in /Database folder

Steps to run the database:

1. Build the docker image

```
docker build -t bookmarks-db .
```

2. Run the container in a detached mode with a postgres password (this will automatically belong to the `postgres` user)

```
docker run -e POSTGRES_PASSWORD=mysecretpassword -d bookmarks-db
```

3. Exec into the container database and automatically connect to bookmarks-db:

```
docker exec -u postgres -it <container-id> psql --dbname=bookmarks-db
```