using System.Security.Claims;
using System.Text;
using LightNote.Application.BusinessLogic.Identity.Commands;
using LightNote.Dal;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using LightNote.Domain.Models.User;
using LightNote.Application.Options;
using Microsoft.Extensions.Options;
using LightNote.Dal.Contracts;
using LightNote.Application.Helpers;
using LightNote.Application.Exceptions;
using System.Collections.Generic;

namespace LightNote.Application.BusinessLogic.Identity.CommandHandlers
{
    public class RegisterIdentityHandler : IRequestHandler<RegisterIdentity, OperationResult<string>>
	{  
        private readonly IOptions<JwtSettings> _jwtOptions;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
		public RegisterIdentityHandler(UserManager<IdentityUser> userManager, IOptions<JwtSettings> jwtOptions, IUnitOfWork unitOfWork)
		{
            _userManager = userManager;
            _jwtOptions = jwtOptions;
            _unitOfWork = unitOfWork;
		}
        private string GenerateJwtToken(string userId, string email) {
            var signingKey = _jwtOptions.Value.SigningKey;
            var issuer = _jwtOptions.Value.Issuer;
            var audience = _jwtOptions.Value.Audiences[0];
            // Set JWT claims
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, userId),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.Email, email),
        
    };

            // Set JWT security key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));

            // Set JWT signing credentials
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Set JWT expiration
            var expiration = DateTime.UtcNow.AddDays(7);

            // Create JWT token
            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            // Return JWT as string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<OperationResult<string>> Handle(RegisterIdentity request, CancellationToken cancellationToken)
        {
            var existingIdentity = await _userManager.FindByEmailAsync(request.Email);
            if (existingIdentity != null) {
                var operationResult = OperationResult<string>.CreateFailure(new []{ new ResourceAlreadyExistsException("User exists")});
                return operationResult;
            }
            var newIdentity = new IdentityUser {
                Email = request.Email,
                UserName = request.Email
            };

            // Create transaction
            using (var transaction = _unitOfWork.BeginTransaction())
            {                 
                try
                {
                    // create identity
                    var result = await _userManager.CreateAsync(newIdentity, request.Password);
                    if (!result.Succeeded) {
                        await transaction.RollbackAsync(cancellationToken);
                        var operationResult = OperationResult<string>.CreateFailure(result.Errors.Select(e => new Exception(e.Description)).ToArray());
                        return operationResult;
                    }
                    // create user profile
                    var basicInfo = BasicUserInfo.CreateBasicUserInfo(request.FirstName, request.LastName, request.PhotoUrl, request.Country, request.City);
                    var userProfile = UserProfile.CreateUserProfile(newIdentity.Id, basicInfo);
                    // generate token
                    var token = GenerateJwtToken(newIdentity.Id, newIdentity.Email);

                    _unitOfWork.UserRepository.Insert(userProfile);
                    _unitOfWork.Save();

                    transaction.Commit();
                    return OperationResult<string>.CreateSuccess(token);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    var operationResult = OperationResult<string>.CreateFailure(new[] { new ResourceAlreadyExistsException(ex.Message) });
                    return operationResult;
                }
            }
        }
    }
}

