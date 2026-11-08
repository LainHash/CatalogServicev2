using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using API.Application;
using API.Contract.Settings;
using API.Infrastructure;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace CatalogAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMyServices(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services.AddInfrastructure(configuration);
            services.AddApplication();

            services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.Converters.Add(
                    new JsonStringEnumConverter());
            });

            return services;
        }

        public static IServiceCollection AddJwtConfig(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            var jwtSettings = configuration
                .GetSection("Jwt")
                .Get<JwtSettings>() ?? new JwtSettings();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    // 1. Tự động tải & cache RSA Public Key từ IdentityService qua JWKS Discovery
                    if (!string.IsNullOrWhiteSpace(jwtSettings.MetadataAddress))
                    {
                        options.MetadataAddress = jwtSettings.MetadataAddress;
                        options.RequireHttpsMetadata = false;
                        options.BackchannelHttpHandler = new HttpClientHandler
                        {
                            ServerCertificateCustomValidationCallback =
                                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                        };
                    }

                    var validationParams = new TokenValidationParameters
                    {
                        ValidateIssuer = !string.IsNullOrWhiteSpace(jwtSettings.Issuer),
                        ValidateAudience = !string.IsNullOrWhiteSpace(jwtSettings.Audience),
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        RoleClaimType = ClaimTypes.Role,
                        NameClaimType = ClaimTypes.NameIdentifier
                    };

                    // 2. Fallback nếu truyền trực tiếp PublicKeyPem trong .env
                    if (!string.IsNullOrWhiteSpace(jwtSettings.PublicKeyPem))
                    {
                        var rsa = RSA.Create();
                        rsa.ImportFromPem(jwtSettings.PublicKeyPem.Replace("\\n", "\n"));
                        validationParams.IssuerSigningKey = new RsaSecurityKey(rsa);
                    }

                    options.TokenValidationParameters = validationParams;

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            if (string.IsNullOrEmpty(context.Token)
                                && !context.Request.Headers.ContainsKey("Authorization")
                                && context.Request.Cookies.TryGetValue("access_token", out var token)
                                && !string.IsNullOrEmpty(token))
                            {
                                context.Token = token;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization();

            return services;
        }

        public static IServiceCollection AddCustomSwagger(
            this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Catalog Service",
                    Version = "v1"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Nhập JWT token."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }

        public static IServiceCollection AddEnvConfig(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            Env.Load();

            var searchDir = Directory.GetCurrentDirectory();
            while (searchDir is not null)
            {
                var envPath = Path.Combine(searchDir, ".env");
                if (File.Exists(envPath))
                {
                    Env.Load(envPath);
                    break;
                }
                searchDir = Directory.GetParent(searchDir)?.FullName;
            }

            configuration.AddEnvironmentVariables();

            return services;
        }
    }
}
