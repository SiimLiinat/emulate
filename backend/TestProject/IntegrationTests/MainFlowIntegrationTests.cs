using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using DAL.App.DTO;
using Domain.App.Identity;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using TestProject.Helpers;
using WebApp;
using Xunit;
using Xunit.Abstractions;

namespace TestProject.IntegrationTests
{
    public class MainFlowIntegrationTests : IClassFixture<CustomWebApplicationFactory<WebApp.Startup>>
    {
        private readonly CustomWebApplicationFactory<WebApp.Startup> _factory;
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _testOutputHelper;

        public MainFlowIntegrationTests(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
        {
            _factory = factory;
            _testOutputHelper = testOutputHelper;
            _client = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.UseSetting(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
                })
                .CreateClient(new WebApplicationFactoryClientOptions()
                    {
                        // dont follow redirects
                        AllowAutoRedirect = false
                    }
                );
        }
        
        [Fact]
        public async Task TestAction_HasSuccessStatusCode()
        {
            // ARRANGE
            var uri = "/api/v1/games";
            
            // ACT
            var getTestResponse = await _client.GetAsync(uri);
            
            // ASSERT
            getTestResponse.EnsureSuccessStatusCode();
            Assert.InRange((int)getTestResponse.StatusCode, 200, 299);
        }
        
        [Fact]
        public async Task Register()
        {
            var register = JsonSerializer.Serialize( new PublicApi.DTO.v1.Register()
                {
                 UserName = "TestUser",
                 Email = "testuser@gmail.com",
                 Password = "TestUser1_"
                });
            var stringContent = new StringContent(register, Encoding.UTF8, "application/json");
            var getRegisterResponse = await _client.PostAsync("/api/v1/account/register", stringContent);
            getRegisterResponse.EnsureSuccessStatusCode();
            
        }
        
        [Fact]
        public async Task Login()
        {
            var login = JsonSerializer.Serialize( new PublicApi.DTO.v1.Login()
            {
                UserName = "TestUser",
                Password = "TestUser1_"
            });
            var stringContent = new StringContent(login, Encoding.UTF8, "application/json");
            var getLoginResponse = await _client.PostAsync("/api/v1/account/login", stringContent);
            getLoginResponse.EnsureSuccessStatusCode();
            var result = getLoginResponse.Content.ReadAsStringAsync().Result;
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + JObject.Parse(result)["token"]);
        }
        
        public async Task<JwtSecurityToken> GetToken()
        {
            var login = JsonSerializer.Serialize( new PublicApi.DTO.v1.Login()
            {
                UserName = "TestUser",
                Password = "TestUser1_"
            });
            var stringContent = new StringContent(login, Encoding.UTF8, "application/json");
            var getLoginResponse = await _client.PostAsync("/api/v1/account/login", stringContent);
            getLoginResponse.EnsureSuccessStatusCode();
            var result = getLoginResponse.Content.ReadAsStringAsync().Result;
            var token = new JwtSecurityTokenHandler().ReadJwtToken(JObject.Parse(result)["token"].ToString());
            return token;
        }

        public Guid GetId(JwtSecurityToken token)
        {
            return Guid.Parse(token.Claims.First().Value);
        }

        [Fact]
        public async Task Main_Flow()
        {
            // ARRANGE
            var uri = "/api/v1/Games";

            // ACT
            var getResponse = await _client.GetAsync(uri);

            // ASSERT
            getResponse.EnsureSuccessStatusCode();

            var body = await getResponse.Content.ReadAsStringAsync();
            // Fixes 'The JSON value could not be converted to System.DateTime'
            body = body.Replace(" ", "T");
            var data = JsonHelper.DeserializeWithWebDefaults<List<DAL.App.DTO.Game>>(body);

            Assert.NotNull(data);
            Assert.NotEmpty(data);
            Assert.Equal("TestGame", data![0].Name);

            var register = JsonSerializer.Serialize(new PublicApi.DTO.v1.Register()
            {
                UserName = "TestUser2",
                Email = "testuser2@gmail.com",
                Password = "TestUser2_"
            });
            var registerContent = new StringContent(register, Encoding.UTF8, "application/json");
            var getRegisterResponse = await _client.PostAsync("/api/v1/account/register", registerContent);
            getRegisterResponse.EnsureSuccessStatusCode();

            var login = JsonSerializer.Serialize(new PublicApi.DTO.v1.Login()
            {
                UserName = "TestUser2",
                Password = "TestUser2_"
            });
            var stringContent = new StringContent(login, Encoding.UTF8, "application/json");
            var getLoginResponse = await _client.PostAsync("/api/v1/account/login", stringContent);
            getLoginResponse.EnsureSuccessStatusCode();
            var result = getLoginResponse.Content.ReadAsStringAsync().Result;
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + JObject.Parse(result)["token"]);

            var token = new JwtSecurityTokenHandler().ReadJwtToken(JObject.Parse(result)["token"].ToString());
            var guid = GetId(token);
            
            var newProgress = JsonSerializer.Serialize(new PublicApi.DTO.v1.APIs.ProgressAdd()
            {
                GameId = data![0].Id,
                AppUserId = guid,
                Time = 10,
                Score = 10,
                CreatedAt = DateTime.Now,
            });
            var progressContent = new StringContent(newProgress, Encoding.UTF8, "application/json");

            var getProgressResponse = await _client.PostAsync("/api/v1/progresses/", progressContent);
            getProgressResponse.EnsureSuccessStatusCode();

            var getProgressesResponse = await _client.GetAsync("/api/v1/progresses/");
            var progressResult = getProgressesResponse.Content.ReadAsStringAsync().Result;
            _testOutputHelper.WriteLine(progressResult.Replace("[", "").Replace("]", ""));
            getProgressesResponse.EnsureSuccessStatusCode();

            var progress =
                Newtonsoft.Json.JsonConvert.DeserializeObject<List<PublicApi.DTO.v1.APIs.Progress>>(progressResult);
            _testOutputHelper.WriteLine(progress!.Count().ToString());
            Assert.NotNull(progress);
            Assert.NotEmpty(progress!);
            Assert.Single(progress!);
            var entity = progress.First();
            Assert.NotNull(entity);
            Assert.Equal("TestGame", entity.GameName);
            Assert.Equal(10, entity.Score);
            Assert.Equal(10, entity.Time!);
        }
    }
}