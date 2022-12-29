using FluentAssertions;
using LightNote.Application.BusinessLogic.Users.Queries;
using LightNote.Domain.Models.User;
using NUnit.Framework;

namespace LightNote.Tests.Unit.Application.Identity.Queries
{
    using static TestBase;
    [TestFixture]
    public class GetAllUsersTest
    {
        [Test]
        public async Task GetAllUsersTest_ReturnsAllUsers() {
            // Arrange
            await AddAsync(UserProfile
				.CreateUserProfile(string.Empty,
									BasicUserInfo.CreateBasicUserInfo("TestFirstName", "TestLastName", "photo", "Country", "City")));

            var query = new GetAllUsers();

            // Act
            var result = await SendAsync(query);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
        }

        [Test]
        public async Task GetAllUsersTest_ReturnsAllUsers_()
        {
            // Arrange
            await AddAsync(UserProfile
                .CreateUserProfile(string.Empty,
                                    BasicUserInfo.CreateBasicUserInfo("TestFirstName", "TestLastName", "photo", "Country", "City")));

            var query = new GetAllUsers();

            // Act
            var result = await SendAsync(query);

            //// Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
        }

        [TearDown]
        public async Task RunAfterEachTestAsync()
        {
            await ResetState();
        }
    }
}

