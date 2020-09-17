using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bookmark.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Moq;
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

            Assert.Contains(expectedBrand, response.Headers.GetValues("copyright"));
        }
    }
}
