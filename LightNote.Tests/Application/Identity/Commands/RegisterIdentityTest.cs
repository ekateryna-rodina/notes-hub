using LightNote.Application.BusinessLogic.Identity.CommandHandlers;
using LightNote.Application.Contracts;
using LightNote.Dal.Contracts;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;

namespace LightNote.UnitTests.Application.Identity.Commands
{
    [TestFixture]
    public class RegisterIdentityTest
    {
        private Mock<IToken> _tokenServiceMock;
        private Mock<UserManager<IdentityUser>> _userManagerMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private RegisterIdentityHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _tokenServiceMock = new Mock<IToken>();
            _userManagerMock = new Mock<UserManager<IdentityUser>>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new RegisterIdentityHandler(_userManagerMock.Object, _unitOfWorkMock.Object, _tokenServiceMock.Object);
        }
	}
}

