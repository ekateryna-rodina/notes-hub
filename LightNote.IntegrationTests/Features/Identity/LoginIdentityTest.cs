using System;
using LightNote.Api.Contracts.Identity.Request;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using LightNote.Api.Contracts.Identity.Response;
using LightNote.Api;
using LightNote.IntegrationTests.Setup;

namespace LightNote.IntegrationTests.Features.Identity
{
    using static TestBase;
	public class LoginIdentityTest
	{
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            _client = new HttpClient();
        }

        [Test]
        public async Task TestLogin()
        {
            var loginRequest = new Login
            {
                Email = "test@example.com",
                Password = "testpassword"
            };

            var json = JsonConvert.SerializeObject(loginRequest);

            // Create an HTTP POST request with the login request JSON in the request body
            var route = "https://localhost:36617/api/v1/Identity/login";
            var request = new HttpRequestMessage(HttpMethod.Post, route)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            // Send the request and get the response
            var response = await _client.SendAsync(request);

            // Assert that the response status code is 200 OK
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            // Deserialize the response body to an AuthenticationResult object
            var authResult = JsonConvert.DeserializeObject<AuthenticationResult>(await response.Content.ReadAsStringAsync());

            // Assert that the token property is not null or empty
            Assert.NotNull(authResult.Token);
        }
    }
}

