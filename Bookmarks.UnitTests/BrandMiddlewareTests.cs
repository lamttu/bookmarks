using System;
using System.Threading.Tasks;
using Bookmark.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Bookmarks.UnitTests
{
    public class BrandMiddlewareTests
    {
        [Fact]
        public async Task BrandMiddleware_ShouldAddBrandForGetRequest()
        {
            using var host = await new HostBuilder()
                .ConfigureWebHost(webBuilder =>
                {
                    webBuilder
                        .UseTestServer()
                        .Configure(app =>
                        {
                            app.UseMiddleware<BrandMiddleware>();
                        });
                })
                .StartAsync();

            var expectedBrand = "LamBookmarks2020";

            var response = await host.GetTestClient().GetAsync($"/bookmarks/{Guid.NewGuid()}");

            Assert.True(response.Headers.Contains("copyright"));
            Assert.Contains(expectedBrand, response.Headers.GetValues("copyright"));
        }

        [Theory]
        [InlineData("POST")]
        [InlineData("DELETE")]
        [InlineData("PUT")]
        [InlineData("PATCH")]
        public async Task BrandMiddleware_ShouldNotAddBrandForPostRequest(string method)
        {
            using var host = await new HostBuilder()
                .ConfigureWebHost(webBuilder =>
                {
                    webBuilder
                        .UseTestServer()
                        .Configure(app =>
                        {
                            app.UseMiddleware<BrandMiddleware>();
                        });
                })
                .StartAsync();

            var server = host.GetTestServer();

            var context = await server.SendAsync(httpContext =>
            {
                httpContext.Request.Method = method;
                httpContext.Request.Path = "/bookmarks";
            });

            Assert.False(context.Response.Headers.ContainsKey("copyright"));
        }
    }
}
