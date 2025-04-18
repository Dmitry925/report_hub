using System.Text.Json.Serialization;
using AutoMapper;
using Exadel.ReportHub.Common.Providers;
using Exadel.ReportHub.Host.Infrastructure.Filters;
using Exadel.ReportHub.Host.Registrations;
using Exadel.ReportHub.RA;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;

namespace Exadel.ReportHub.Host;

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        const string scopeName = "report_hub_api";
        const string scopeDescription = "Full access to Report Hub API";
        const string authority = "Authority";

        services.AddControllers(options =>
        {
            options.Filters.Add<ExceptionFilter>();
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        services.AddSwaggerGen(c =>
        {
            const string apiVersion = "v1";

            var tokenUrl = new Uri($"{configuration[authority]}/connect/token");

            c.SwaggerDoc(apiVersion, new OpenApiInfo { Title = "ReportHubAPI", Version = apiVersion });
            c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    ClientCredentials = new OpenApiOAuthFlow
                    {
                        TokenUrl = tokenUrl,
                        Scopes = new Dictionary<string, string>
                        {
                            { scopeName, scopeDescription }
                        }
                    },
                    Password = new OpenApiOAuthFlow
                    {
                        TokenUrl = tokenUrl,
                        Scopes = new Dictionary<string, string>
                        {
                            { scopeName, scopeDescription }
                        }
                    }
                }
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                        }
                    },
                    new[] { scopeName }
                }
            });
        });
        services.AddAuthentication()
            .AddJwtBearer(options =>
            {
                options.Authority = configuration[authority];
                options.Audience = scopeName;
                options.RequireHttpsMetadata = true;
            });

        services.AddAuthorization();

        services.AddIdentity(configuration);
        services.AddMongo();
        services.AddMediatR();
        services.AddAutoMapper(typeof(Startup));
        services.AddHttpContextAccessor();
        services.AddScoped<IUserProvider, UserProvider>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMapper mapper)
    {
        mapper.ConfigurationProvider.AssertConfigurationIsValid();

        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Report Hub API"));
        app.UseRouting();

        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedProto
        });

        app.UseIdentityServer();

        app.Use(async (context, next) =>
        {
            if (context.Request.Path.StartsWithSegments("/.well-known"))
            {
                Console.WriteLine($"Request to: {context.Request.Path}");
            }

            await next();
        });

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
