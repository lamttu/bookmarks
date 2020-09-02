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

## Learnings

1. How to do integration tests in .NET Core using [WebApplicationFactory](https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-3.1)