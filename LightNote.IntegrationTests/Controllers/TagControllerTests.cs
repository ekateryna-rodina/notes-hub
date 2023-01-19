using System;
using System.Net;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using FluentAssertions;
using LightNote.Api;
using LightNote.Api.Contracts.Identity.Request;
using LightNote.Api.Contracts.Identity.Response;
using LightNote.Api.Contracts.Tag.Request;
using LightNote.Api.Contracts.Tag.Response;
using LightNote.IntegrationTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LightNote.IntegrationTests.Controllers
{
    using static TestUtils;
	public class TagController : IntegrationTestBase
	{
        private const string version = "1";
        private const string controllerName = "Tag";
        private readonly string BaseRoute = $"api/v{version}/{controllerName}/";

        [Test]
		public async Task GetAll_WithNotTags_ReturnsOkWithEmptyCollection() {
            // Arrange
            await AuthenticateAsync();
            // Act
            var response = await HttpClient.GetAsync(BaseRoute);
            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Test]
        public async Task GetAll_WithFewTags_ReturnsOkWithPayload()
        {
            // Arrange
            await AuthenticateAsync();
            var tags = (new List<string>() { "biology", "coding", "productivity" });
            var createTagsRequest = new CreateTagRequest { Names = tags };
            await HttpClient.PostAsJsonAsync(BaseRoute, createTagsRequest);
            // Act
            var response = await HttpClient.GetAsync(BaseRoute + ApiRoutes.Tag.GetAll);
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<TagResponse>>(responseContent);
            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result.Should().NotBeNull();
            result!.Count().Should().Be(tags.Count);
        }

        [Test]
        public async Task CreateAsync_WithValidTag_ReturnsCreated()
        {
            // Arrange
            await AuthenticateAsync();
            var tagName = "productivity";
            var createTagsRequest = new CreateTagRequest { Names = new List<string>() { tagName } };
            // Act
            var result = await HttpClient.PostAsJsonAsync(BaseRoute, createTagsRequest);
            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.Created);
            result.Headers.Location.Should().NotBeNull();
            result.Headers.Location!.OriginalString.Should().NotBeEmpty();
        }

        [Test]
        public async Task CreateAsync_WithNoName_ReturnsBadRequest()
        {
            // Arrange
            await AuthenticateAsync();
            var tagName = "";
            var createTagsRequest = new CreateTagRequest { Names = new List<string>() { tagName } };
            // Act
            var result = await HttpClient.PostAsJsonAsync(BaseRoute, createTagsRequest);
            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task GetByIdsAsync_GivenValidIds_ReturnsOkAndRequestedTags()
        {
            // Arrange
            await AuthenticateAsync();
            var tags = (new List<string>() { "biology", "coding", "productivity" });
            var createTagsRequest = new CreateTagRequest { Names = tags };
            var result = await HttpClient.PostAsJsonAsync(BaseRoute, createTagsRequest);
            var input = result.Headers.Location!.ToString();
            Regex regex = new Regex(@"^.*api\/v1\/Tag\/(.*)$");
            Match match = regex.Match(input);
            string allIdsCreated = match.Groups[1].Value;
            int lastIndex = allIdsCreated.LastIndexOf("|");
            string ids = allIdsCreated.Substring(0, lastIndex);
            // Act
            var response = await HttpClient.GetAsync(BaseRoute + ApiRoutes.Tag.GetByIds.Replace("{ids}", ids));
            // Assert
            var responseContent = await response.Content.ReadAsStringAsync();
            var contentResult = JsonConvert.DeserializeObject<IEnumerable<TagResponse>>(responseContent);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var biologyGuid = Guid.Parse(ids.Split("|")[0]);
            var codingGuid = Guid.Parse(ids.Split("|")[1]);
            contentResult.Should().HaveCount(2, "because there should be two items in the collection");
            contentResult.Should().Contain(r => r.Id == biologyGuid && r.Name == "biology", "because the first item should have biologyGuid and name biology");
            contentResult.Should().Contain(r => r.Id == codingGuid && r.Name == "coding", "because the second item should have codingGuid and name coding");
        }

        [TearDown]
        public async Task CleanupAsync()
        {
            await ResetState();
        }
    }
}

