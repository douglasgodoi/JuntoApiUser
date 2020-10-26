using JuntoApi;
using JuntoApi.Data;
using JuntoApi.Models;
using JuntoApi.Repositories;
using JuntoApi.Services;
using JuntoApi.Test;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace JuntoApiTest
{
    public class IntegrationTestFixture
    {
        public readonly HttpClient _client;

        public IntegrationTestFixture()
        {
            var serviceCollection = new ServiceCollection();

            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(StoreDataContext));
                        services.AddEntityFrameworkInMemoryDatabase();
                        services.AddDbContext<StoreDataContext>((sp, options) =>
                        {
                            options.UseInMemoryDatabase("TestDb").UseInternalServiceProvider(sp);
                        });
                        services.AddTransient<IUserRepository, UserRepository>();

                        serviceCollection = (ServiceCollection)services;
                    });
                });

            _client = appFactory.CreateClient();

            ApiRoutes._baseUrl = _client.BaseAddress.AbsoluteUri;
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
        public ServiceProvider ServiceProvider { get; private set; }

        protected async Task AuthenticateAsync()
        {
            var user = new User()
            {
                UserName = "admin",
                Password = "1234",
                Role = "Admin"
            };
            var token = TokenService.GenerateToken(user);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }
    }
}
