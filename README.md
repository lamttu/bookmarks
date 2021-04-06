# Bookmarks

This is an api that will return articles linked with a bookmark.

## Supported endpoints

- [x] GET /bookmarks
- [x] GET /bookmarks/id
- [x] POST /bookmarks
- [x] DELETE /bookmarks/id

## Todo
- [x] Handle GET request where the bookmark doesn't exist
- [x] Middleware to add header to add "copyright" header to GET requests
- [x] [Option pattern](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-3.1) vs [Configuration](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-3.1)
  - IOptions allows for strongly typed configuration.
- [x] [Service lifetime](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1#service-lifetimes)
- [x] Use Dapper to connect to a dockerised PostgresDB
- [x] Multistage Dockerfile for a lighter final image
- [x] Deploy this to a k8s cluster

## Learnings

### 1. How to do integration tests in .NET Core using [WebApplicationFactory](https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-3.1)
### 2. How to TDD a [custom middleware](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/write?view=aspnetcore-3.1) in .NET using [TestServer](https://docs.microsoft.com/en-us/aspnet/core/test/middleware?view=aspnetcore-3.1). 

**Remember**: The point of TDD is to add in logic. Normally, the beginning (barebone of a file) is an empty file without anything. However, in the case of a custom middleware in .NET core, a bare bone middleware has the `RequestDeletegate` injected in the constructor and the `InvokeAsync(HttpContext context)`. When you create your `MyCustomMiddleware`, it's okay to include those. The logic goes into `InvokeAsync`. Write an empty `InvokeAsync` and take it from there.


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

## Deploy api to local k8s (docker desktop)

1. Build the local docker image for the api

`docker build -t bookmarks-api .`

2. Create a k8s deployment

`kubectl apply -f ./k8s/api-deployment.yml`

3. Create a k8s service that exposes the deployment externally

`kubectl apply -f ./k8s/api-service.yml`