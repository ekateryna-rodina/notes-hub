using LightNote.Application.Contracts;
using LightNote.Application.Options;
using LightNote.Application.Services.TokenGenerators;
using LightNote.Application.Services.TokenValidators;
using LightNote.Dal.Contracts;
using LightNote.Dal.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace LightNote.Api.Registrars
{
    public class IdentityRegistrar : IWebAppBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            var jwtSettings = new JwtSettings();
            builder.Configuration.Bind(nameof(JwtSettings), jwtSettings);

            var jwtConfig = builder.Configuration.GetSection(nameof(JwtSettings));
            builder.Services.Configure<JwtSettings>(jwtConfig);

            builder.Services.AddAuthentication(a =>
            {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(jwt =>
                {
                    jwt.SaveToken = true;
                    jwt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(jwtSettings.AccessSigningKey)),
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidateAudience = true,
                        ValidAudiences = jwtSettings.Audiences,
                        RequireExpirationTime = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                    jwt.Audience = jwtSettings.Audiences[0];
                    jwt.ClaimsIssuer = jwtSettings.Issuer;
                });
            builder.Services.AddTransient<ITokenGenerator, TokenGenerator>();
            builder.Services.AddTransient<ITokenValidator, RefreshTokenValidator>();
            builder.Services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            builder.Services.AddTransient<IAuthenticator, Authenticator>();
            builder.Services.AddSingleton<AccessTokenGenerator>();
            builder.Services.AddSingleton<RefreshTokenGenerator>();
        }
    }
}

