using FluentAssertions;
using JuntoApi.Controllers;
using JuntoApi.Models;
using JuntoApi.Repositories;
using JuntoApi.ViewModels;
using JuntoApiTest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JuntoApi.Test
{
    public class UserRepositoryTests : IClassFixture<IntegrationTestFixture>
    {
        private readonly IUserRepository _userRepository;

        public UserRepositoryTests(IntegrationTestFixture fixture)
        {
            _userRepository = fixture.ServiceProvider.GetRequiredService<IUserRepository>();
        }

        #region Insert
        [Fact(DisplayName ="User - Insert Test.")]
        public void Save_Return_OkResult()
        {
            //Arrange
            var user = new User()
            {
                UserName = "testSave",
                Password = "1234"
            };
            var inserted = _userRepository.Save(user);

            //Assert  
            inserted.Should().BeOfType<User>().And.NotBeNull();
        }
        #endregion

        #region Update
        [Fact(DisplayName = "User - Update Test.")]
        public void Update_Return_OkResult()
        {
            //Arrange
            var user = new User()
            {
                UserName = "testUpdate",
                Password = "1234"
            };
            var inserted = _userRepository.Save(user);
            inserted.Role = "Admin";

            //Act
            var updated = _userRepository.Update(inserted);

            //Assert  
            updated.Should().BeOfType<User>().And.NotBeNull();
            updated.Role.Should().Equals("Admin");
        }
        #endregion

        #region Get
        [Fact(DisplayName ="User - Get Test.")]
        public void Get_Return_OkResult()
        {
            //Arrange
            var user = new User()
            {
                UserName = "testGet",
                Password = "1234"
            };
            _userRepository.Save(user);

            //Act  
            var lstUsers = _userRepository.Get();

            //Assert  
            lstUsers.Should().NotBeEmpty();
        }
        #endregion

        #region Delete
        [Fact(DisplayName = "User - Delete Test.")]
        public void Delete_Return_OkResult()
        {
            //Arrange
            var user = new User()
            {
                UserName = "testDelete",
                Password = "1234"
            };
            _userRepository.Save(user);

            //Act  
            var deleted = _userRepository.Delete(user);
            var result  = _userRepository.Find("testDelete", "1234");

            //Assert  
            result.Should().BeNull();
        }
        #endregion

        #region Find
        [Fact(DisplayName = "User - Find Test.")]
        public void Find_Return_OkResult()
        {
            //Arrange
            var user = new User()
            {
                UserName = "testFind",
                Password = "1234"
            };
            _userRepository.Save(user);

            //Act  
            var result = _userRepository.Find("testFind", "1234");

            //Assert  
            result.Should().NotBeNull();
        }
        #endregion
    }
}
