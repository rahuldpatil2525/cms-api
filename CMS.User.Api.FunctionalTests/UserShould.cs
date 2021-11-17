using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CMS.User.Api.FunctionalTests.Extensions;
using CMS.User.Api.RequestModels;
using CMS.User.Api.ResponseModels;
using FluentAssertions;
using Xunit;

namespace CMS.User.Api.FunctionalTests
{
    public class UserShould : IClassFixture<Factories.UserWebApplicationFactory>
    {
        public UserShould()
        {

        }

        [Fact]
        public async Task Return_Empty_User_List_When_Get_All_Users()
        {
            var factory = new Factories.UserWebApplicationFactory();

            var client = factory.CreateClient();

            var response = await client.GetAsync("/api/user");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var model = await response.Deserialize<IEnumerable<UserResponse>>();

            model.Should().BeEmpty();
        }

        [Fact]
        public async Task Return_Newly_Added_User_When_Post_Valid_User_Data()
        {
            var factory = new Factories.UserWebApplicationFactory();

            var client = factory.CreateClient();

            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var userRequest = new UserRequest()
            {
                UserName = "TestUser"
            };

            var content = new StringContent(JsonSerializer.Serialize(userRequest), Encoding.UTF8,
                                    "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, "/api/user");

            request.Content = content;

            var response = await client.SendAsync(request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var model = await response.Deserialize<UserResponse>();

            model.Should().NotBeNull();
        }
    }
}
