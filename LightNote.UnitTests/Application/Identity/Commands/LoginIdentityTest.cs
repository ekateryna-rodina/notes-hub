﻿using System.Linq.Expressions;
using LightNote.Application.BusinessLogic.Identity.CommandHandlers;
using LightNote.Application.BusinessLogic.Identity.Commands;
using LightNote.Application.Exceptions;
using LightNote.Application.Services.TokenGenerators;
using LightNote.Dal.Contracts;
using LightNote.Dal.Repository;
using LightNote.Domain.Models.User;
using LightNote.UnitTests.Mocks;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;

namespace LightNote.UnitTests.Application.Identity.Commands
{
    [TestFixture]
    public class LoginIdentityTest
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<AccessTokenGenerator> _accessTokenGeneratorMock;
        private Mock<RefreshTokenGenerator> _refreshTokenGeneratorMock;
        private Mock<UserManager<IdentityUser>> _userManagerMock;
        private LoginIdentityHandler _handler;
        private readonly List<IdentityUser> _users = new List<IdentityUser>
                 {
                      new IdentityUser("user1@test.com"){
                          Id = "identityId1"
                      }
                 };

        [SetUp]
        public void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _accessTokenGeneratorMock = new Mock<AccessTokenGenerator>();
            _refreshTokenGeneratorMock = new Mock<RefreshTokenGenerator>();
            // TODO: fix this test
            // _tokenServiceMock.Setup(x => x.GenerateClaimsAndJwtToken(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<string>())).Returns(new JwtSecurityToken());
            _userManagerMock = UserManagerMock.Create<IdentityUser>(_users);
            _handler = new LoginIdentityHandler(_accessTokenGeneratorMock.Object, _refreshTokenGeneratorMock.Object, _userManagerMock.Object, _unitOfWorkMock.Object);
        }

        [Test]
        public async Task Handle_UserDoesNotExist_ReturnsFailureResult()
        {
            // Arrange
            var request = new LoginIdentity { Email = "test@example.com", Password = "password" };
            _userManagerMock.Setup(x => x.FindByEmailAsync(request.Email)).ReturnsAsync((IdentityUser)null);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<ResourceNotFoundException>(result.Exceptions.First());
            Assert.AreEqual("User is not registered", result.Exceptions.First().Message);
        }

        [Test]
        public async Task Handle_PasswordIsIncorrect_ReturnsFailureResult()
        {
            // Arrange
            var request = new LoginIdentity { Email = "user1@test.com", Password = "wrongPassword" };
            _userManagerMock.Setup(x => x.FindByEmailAsync(request.Email)).ReturnsAsync(_users.FirstOrDefault());
            _userManagerMock.Setup(u => u.CheckPasswordAsync(_users.FirstOrDefault(), request.Password)).ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.IsNull(result.Value);
            Assert.IsInstanceOf<IncorrectPasswordException>(result.Exceptions.First());
            Assert.AreEqual("Login is incorrect", result.Exceptions.First().Message);
        }

        [Test]
        public async Task Handle_ValidUser_ReturnsSuccessResult()
        {
            // Arrange
            var password = "password1";
            var request = new LoginIdentity { Email = "user1@test.com", Password = password };

            var passwordHasher = new PasswordHasher<IdentityUser>();
            _userManagerMock.Setup(x => x.FindByEmailAsync(request.Email)).ReturnsAsync(_users.FirstOrDefault());
            _userManagerMock.Setup(u => u.CheckPasswordAsync(_users.FirstOrDefault(), request.Password)).ReturnsAsync(true);
            var userRepositoryMock = new Mock<GenericRepository<UserProfile>>();
            var users = new List<UserProfile>() {
                UserProfile.CreateUserProfile(string.Empty, BasicUserInfo.CreateBasicUserInfo("fn", "ln", "photo", "country", "city"))
            };
            userRepositoryMock
                .Setup(m => m.Get(It.IsAny<Expression<Func<UserProfile, bool>>>(),
                        It.IsAny<Func<IQueryable<UserProfile>, IOrderedQueryable<UserProfile>>>(),
                        It.IsAny<string>())).ReturnsAsync(users);
            _unitOfWorkMock.Setup(x => x.UserRepository).Returns(userRepositoryMock.Object);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.NotNull(result.Value);
        }
    }
}

