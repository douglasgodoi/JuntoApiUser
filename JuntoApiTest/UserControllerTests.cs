using FluentAssertions;
using JuntoApi.Models;
using JuntoApi.ViewModels;
using JuntoApiTest;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace JuntoApi.Test
{
    public class UserControllerTests: IntegrationTestFixture
    {
        #region ChangePassword
        [Theory(DisplayName = "User - Change password - BadRequest tests.")]
        [InlineData("invalidUser", "1234", "", "")]
        [InlineData("admin", "1234", "", "")]
        [InlineData("admin", "1234", "qwer", "1q2w")]
        public async void ChangePassword_BadRequest(string name, string pass, string newPass, string confirmPass)
        {
            //Arrange
            var model = new StringContent(JsonConvert.SerializeObject(new ChangePasswordViewModel()
            {
                UserName = name,
                CurrentPassword = pass,
                NewPassword = newPass,
                ConfirmPassword = confirmPass
            }),
            Encoding.UTF8, "application/json");

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
            var change = await _client.PutAsync(ApiRoutes.Users.ChangePassword, model);

            //Assert  
            change.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory(DisplayName = "User - Change password - Ok Test.")]
        [InlineData("admin", "1234", "qwert", "qwert")]
        public async void ChangePassword_OkResult(string name, string pass, string newPass, string confirmPass)
        {
            //Arrange
            var model = new StringContent(JsonConvert.SerializeObject(new ChangePasswordViewModel()
            {
                UserName = name,
                CurrentPassword = pass,
                NewPassword = newPass,
                ConfirmPassword = confirmPass
            }),
            Encoding.UTF8, "application/json");

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
            var response = await _client.PutAsync(ApiRoutes.Users.ChangePassword, model);

            var datastring = (response.Content.ReadAsStringAsync().Result);
            var userResponse = JsonConvert.DeserializeObject<User>(datastring);

            //Assert  
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            userResponse.Password.Should().Be("qwert");
        }
        #endregion
    }
}
