using System;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace LightNote.UnitTests.Mocks
{
    public static class UserManagerMock
    {
        public static Mock<UserManager<TUser>> Create<TUser>(List<TUser> ls) where TUser : class
        {
            var passwordHasher = new Mock<IPasswordHasher<TUser>>();
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, passwordHasher.Object, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => ls.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);

            return mgr;
        }
    }
}

