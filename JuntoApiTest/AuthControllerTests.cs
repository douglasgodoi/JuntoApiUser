using FluentAssertions;
using JuntoApi.Models;
using JuntoApiTest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JuntoApi.Test
{
    public class AuthControllerTests : IntegrationTestFixture
    {
        [Fact]
        public async Task Login_ValidUser()
        {
            //Arrange
            var content = new StringContent(JsonConvert.SerializeObject(new User()
            {
                UserName = "admin",
                Password = "1234",
                Role = "Admin",
            }),
            Encoding.UTF8,
            "application/json");
            
            //Save in memory
            await _client.PostAsync(ApiRoutes.Users.Post, content);

            //Act
            var response = await _client.PostAsync(ApiRoutes.Auth.Login, content);

            //Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact(DisplayName = "Authentication - Restrict Method - Unauthorized Test.")]
        public async Task RestrictMethod_UnauthorizedTest()
        {
            //Act
            var response = await _client.GetAsync(ApiRoutes.Auth.Authenticated);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact(DisplayName ="Authentication - Restrict Method - Ok Test.")]
        public async Task RestrictMethod_OkTest()
        {
            //Arrange
            await AuthenticateAsync();

            //Act
            var response = await _client.GetAsync(ApiRoutes.Auth.Authenticated);
            var messageResult = (response.Content.ReadAsStringAsync().Result);

            //Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            messageResult.Should().Be("Autenticado - admin");
        }
    }
}
