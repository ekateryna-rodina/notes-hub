using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using LightNote.Api;
using LightNote.Api.Contracts.Identity.Request;
using LightNote.Api.Contracts.Identity.Response;
using LightNote.Api.Utils;
using LightNote.Dal;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace LightNote.IntegrationTests.Setup
{
    using static TestUtils;
    public class IntegrationTestBase
	{
        private const string version = "1";
        private const string controllerName = "Identity";
        private readonly string BaseRoute = $"api/v{version}/{controllerName}/";
        protected async Task AuthenticateAsync() {
			HttpClient.DefaultRequestHeaders.Authorization = new  AuthenticationHeaderValue("bearer", await GetAccessToken());
		}

		
        private async Task<string> GetAccessToken()
        {
			var registerRequest = new RegisterRequest
			{
				FirstName = "Test First Name",
				LastName = "Test Last Name",
				Email = $"testuser{Guid.NewGuid()}@test.com",
				Password = "Qwerty123!",
				Country = "USA",
				City = "Philadelphia",
				PhotoUrl = "avatarihere"

			};
			var route = BaseRoute + ApiRoutes.Identity.Register;
            var result = await HttpClient.PostAsJsonAsync(route, registerRequest);
			var authenticationResponse = await result.Content.ReadFromJsonAsync<AuthenticationResponse>();
			return authenticationResponse!.AccessToken;
        }
    }
}
