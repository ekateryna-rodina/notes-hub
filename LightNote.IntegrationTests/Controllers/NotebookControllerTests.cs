using System.Diagnostics.Contracts;
using System.Net;
using System.Net.Http.Json;
using Azure;
using Docker.DotNet.Models;
using FluentAssertions;
using LightNote.Api;
using LightNote.Api.Contracts.Common;
using LightNote.Api.Contracts.Notebook.Request;
using LightNote.Api.Contracts.Notebook.Response;
using LightNote.Api.Contracts.Tag.Request;
using LightNote.Application.BusinessLogic.Notebook.Commands;
using LightNote.Dal.Contracts;
using LightNote.Domain.Models.Common.ValueObjects;
using LightNote.Domain.Models.UserProfileAggregate;
using LightNote.IntegrationTests.Setup;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LightNote.IntegrationTests.Controllers
{
    using static TestUtils;
    public class NotebookControllerTests : IntegrationTestBase
    {
        private const string version = "1";
        private const string controllerName = "Notebook";
        private readonly string BaseRoute = $"api/v{version}/{controllerName}/";

        [Test]
        public async Task GetAll_WithNotNotebooks_ReturnsOkWithEmptyCollection()
        {
            // Arrange
            await AuthenticateAsync();
            // Act
            var response = await HttpClient.GetAsync(BaseRoute);
            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Test]
        public async Task GetAll_WithMultipleNotebooks_ReturnsOkWithPayload()
        {
            // Arrange
            await AuthenticateAsync();
            var titles = (new List<string>() { "MyWork", "Wellness", "HistoryResearch" });
            foreach (var title in titles)
            {
                var createNotebookRequest = new CreateNotebookRequest { Title = title };
                await HttpClient.PostAsJsonAsync(BaseRoute, createNotebookRequest);
            }
            
            // Act
            var response = await HttpClient.GetAsync(BaseRoute + ApiRoutes.Notebook.GetAll);
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<NotebookResponse>>(responseContent);
            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result.Should().NotBeNull();
            result!.Count().Should().Be(titles.Count);
            result.Should().Contain(r => r.Title == "MyWork");
            result.Should().Contain(r => r.Title == "Wellness");
            result.Should().Contain(r => r.Title == "HistoryResearch");
        }
        [Test]
        public async Task GetById_WithValidId_ReturnsOkWithPayload() {
            // Arrange
            await AuthenticateAsync();
            await HttpClient.PostAsJsonAsync(BaseRoute, new CreateNotebookRequest { Title = "Finance" });
            var arrangeResponse = await HttpClient.GetAsync(BaseRoute + ApiRoutes.Notebook.GetAll);
            var arrangeResponseContent = await arrangeResponse.Content.ReadAsStringAsync();
            var arrageParsed = JsonConvert.DeserializeObject<IEnumerable<NotebookResponse>>(arrangeResponseContent);
            var id = arrageParsed!.FirstOrDefault()!.Id;

            // Act
            var actResponse = await HttpClient.GetAsync(BaseRoute + ApiRoutes.Notebook.GetById.Replace("{id}", id.ToString()));
            var actResponseContent = await actResponse.Content.ReadAsStringAsync();
            var actParsed = JsonConvert.DeserializeObject<NotebookResponse>(actResponseContent);

            // Assert
            actResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            actParsed.Should().NotBeNull();
            actParsed!.Id.Should().Be(id);
        }
        [Test]
        public async Task GetById_WithInvalidId_ReturnsBadRequest()
        {
            // Arrange
            var id = Guid.Empty.ToString();
            await AuthenticateAsync();

            // Act
            var actResponse = await HttpClient.GetAsync(BaseRoute + ApiRoutes.Notebook.GetById.Replace("{id}", id));
            var actResponseContent = await actResponse.Content.ReadAsStringAsync();
            var actParsed = JsonConvert.DeserializeObject<ErrorResponse>(actResponseContent);

            // Assert
            actResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            actParsed!.Errors.Should().Contain("Id cannot be empty");

        }
        [Test]
        public async Task GetById_WithNoAuthorization_ReturnsBadRequest()
        {
            // Arrange
            // Create a notebook by user A
            await AuthenticateAsync();
            await HttpClient.PostAsJsonAsync(BaseRoute, new CreateNotebookRequest { Title = "Finance" });
            var arrangeResponseA = await HttpClient.GetAsync(BaseRoute + ApiRoutes.Notebook.GetAll);
            var arrangeResponseContentA = await arrangeResponseA.Content.ReadAsStringAsync();
            var arrageParsedA = JsonConvert.DeserializeObject<IEnumerable<NotebookResponse>>(arrangeResponseContentA);
            var idA = arrageParsedA!.FirstOrDefault()!.Id;

            // Create a notebook by user B
            await AuthenticateAsync();
            await HttpClient.PostAsJsonAsync(BaseRoute, new CreateNotebookRequest { Title = "History" });

            // Act
            var actResponse = await HttpClient.GetAsync(BaseRoute + ApiRoutes.Notebook.GetById.Replace("{id}", idA.ToString()));
            var actResponseContent = await actResponse.Content.ReadAsStringAsync();
            var actParsed = JsonConvert.DeserializeObject<ErrorResponse>(actResponseContent);

            // Assert
            actResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            actParsed!.Errors.Should().Contain("Access is not authorized");
        }

        [Test]
        public async Task CreateAsync_WithValidNotebook_ReturnsCreated() {
            // Arrange
            await AuthenticateAsync();
            var createNotebookRequest = new CreateNotebookRequest { Title = "History" };
            // Act
            var result = await HttpClient.PostAsJsonAsync(BaseRoute, createNotebookRequest);
            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.Created);
            result.Headers.Location.Should().NotBeNull();
            result.Headers.Location!.OriginalString.Should().NotBeEmpty();
        }

        [Test]
        public async Task CreateAsync_WithInvalidTitle_ReturnsBadRequest()
        {
            // Arrange
            await AuthenticateAsync();
            var createNotebookRequest = new CreateNotebookRequest { Title = "" };
            // Act
            var result = await HttpClient.PostAsJsonAsync(BaseRoute, createNotebookRequest);
            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task UpdateAsync_WithValidNotebook_UpdatesReturnsNoContent()
        {
            // Arrange
            await AuthenticateAsync();
            var createNotebookRequest = new CreateNotebookRequest { Title = "History" };
            var result = await HttpClient.PostAsJsonAsync(BaseRoute, createNotebookRequest);
            var arrangeResponseContent = await result.Content.ReadAsStringAsync();
            var arrangeParsed = JsonConvert.DeserializeObject<NotebookResponse>(arrangeResponseContent);
            var updateRequest = new UpdateNotebookRequest() {
                Id = arrangeParsed!.Id,
                Title = "HistoryUpdated"
            };
            
            // Act
            var actResult = await HttpClient.PutAsJsonAsync(BaseRoute + ApiRoutes.Notebook.Update.Replace("{id}", updateRequest!.Id.ToString()), updateRequest);

            // Assert
            var assertResponse = await HttpClient.GetAsync(BaseRoute + ApiRoutes.Notebook.GetById.Replace("{id}", arrangeParsed.Id.ToString()));
            var assertResponseContent = await assertResponse.Content.ReadAsStringAsync();
            var actParsed = JsonConvert.DeserializeObject<NotebookResponse>(assertResponseContent);

            actResult.StatusCode.Should().Be(HttpStatusCode.NoContent);
            actParsed!.Title.Should().Be(updateRequest.Title);
        }

        [Test]
        public async Task UpdateAsync_WithInValidNotebookId_UpdatesReturnsBadRequest()
        {
            // Arrange
            await AuthenticateAsync();
            var createNotebookRequest = new CreateNotebookRequest { Title = "History" };
            var result = await HttpClient.PostAsJsonAsync(BaseRoute, createNotebookRequest);
            var arrangeResponseContent = await result.Content.ReadAsStringAsync();
            var arrangeParsed = JsonConvert.DeserializeObject<NotebookResponse>(arrangeResponseContent);
            var updateRequest = new UpdateNotebookRequest()
            {
                Id = Guid.NewGuid(),
                Title = "HistoryUpdated"
            };

            // Act
            var actResult = await HttpClient.PutAsJsonAsync(BaseRoute + ApiRoutes.Notebook.Update.Replace("{id}", updateRequest!.Id.ToString()), updateRequest);
            var actResponseContent = await actResult.Content.ReadAsStringAsync();
            var actParsed = JsonConvert.DeserializeObject<ErrorResponse>(actResponseContent);

            // Assert
            actResult.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            actParsed!.Errors.Should().Contain("Notebook not found");
        }

        [Test]
        public async Task UpdateAsync_WithInValidUserId_UpdatesReturnsBadRequest()
        {
            // Arrange
            await AuthenticateAsync();
            var createNotebookRequest = new CreateNotebookRequest { Title = "History" };
            var result = await HttpClient.PostAsJsonAsync(BaseRoute, createNotebookRequest);
            var arrangeResponseContent = await result.Content.ReadAsStringAsync();
            var arrangeParsed = JsonConvert.DeserializeObject<NotebookResponse>(arrangeResponseContent);
            var updateRequest = new UpdateNotebookRequest()
            {
                Id = arrangeParsed!.Id,
                Title = "HistoryUpdated"
            };
            await AuthenticateAsync();

            // Act
            var actResult = await HttpClient.PutAsJsonAsync(BaseRoute + ApiRoutes.Notebook.Update.Replace("{id}", updateRequest!.Id.ToString()), updateRequest);
            var actResponseContent = await actResult.Content.ReadAsStringAsync();
            var actParsed = JsonConvert.DeserializeObject<ErrorResponse>(actResponseContent);

            // Assert
            actResult.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            actParsed!.Errors.Should().Contain("Access is not authorized");
        }

        [Test]
        public async Task DeleteAsync_WithValidNotebook_DeletesAndReturnsNoContent()
        {
            // Arrange
            await AuthenticateAsync();
            var createNotebookRequest = new CreateNotebookRequest { Title = "History" };
            var result = await HttpClient.PostAsJsonAsync(BaseRoute, createNotebookRequest);
            var arrangeResponseContent = await result.Content.ReadAsStringAsync();
            var arrangeParsed = JsonConvert.DeserializeObject<NotebookResponse>(arrangeResponseContent);

            // Act
            var actResult = await HttpClient.DeleteAsync(BaseRoute + ApiRoutes.Notebook.Delete.Replace("{id}", arrangeParsed!.Id.ToString()));

            // Assert
            var assertResponse = await HttpClient.GetAsync(BaseRoute + ApiRoutes.Notebook.GetById.Replace("{id}", arrangeParsed.Id.ToString()));
            var assertResponseContent = await assertResponse.Content.ReadAsStringAsync();
            var actParsed = JsonConvert.DeserializeObject<NotebookResponse>(assertResponseContent);
            actResult.StatusCode.Should().Be(HttpStatusCode.NoContent);
            actParsed.Should().BeNull();
        }

        [Test]
        public async Task DeleteAsync_WithInValidUserId_ReturnsBadRequest()
        {
            // Arrange
            await AuthenticateAsync();
            var createNotebookRequest = new CreateNotebookRequest { Title = "History" };
            var result = await HttpClient.PostAsJsonAsync(BaseRoute, createNotebookRequest);
            var arrangeResponseContent = await result.Content.ReadAsStringAsync();
            var arrangeParsed = JsonConvert.DeserializeObject<NotebookResponse>(arrangeResponseContent);
            await AuthenticateAsync();

            // Act
            var actResult = await HttpClient.DeleteAsync(BaseRoute + ApiRoutes.Notebook.Delete.Replace("{id}", arrangeParsed!.Id.ToString()));
            var actResponseContent = await actResult.Content.ReadAsStringAsync();
            var actParsed = JsonConvert.DeserializeObject<ErrorResponse>(actResponseContent);

            // Assert
            actResult.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            actParsed!.Errors.Should().Contain("Access is not authorized");
        }

        [Test]
        public async Task DeleteAsync_WithInValidNotebookId_ReturnsBadRequest()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var actResult = await HttpClient.DeleteAsync(BaseRoute + ApiRoutes.Notebook.Delete.Replace("{id}", Guid.NewGuid().ToString()));
            var actResponseContent = await actResult.Content.ReadAsStringAsync();
            var actParsed = JsonConvert.DeserializeObject<ErrorResponse>(actResponseContent);

            // Assert
            actResult.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            actParsed!.Errors.Should().Contain("Notebook not found");
        }

        [TearDown]
        public async Task CleanupAsync()
        {
            await ResetState();
        }
    }
}
