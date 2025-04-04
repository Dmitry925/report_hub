﻿using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Exadel.ReportHub.Identity;
using Exadel.ReportHub.Identity.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace Exadel.ReportHub.Host.Registrations;

public static class IdentityServerRegistrations
{
    public static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentityServer(options =>
        {
            //options.IssuerUri = configuration["Authority"];
        })
            .AddClientStore<IdentityClientStore>()
            .AddResourceStore<IdentityResourceStore>()
            .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
            .AddProfileService<IdentityProfileService>()
            .AddDeveloperSigningCredential(false);

        services.AddScoped<IProfileService, IdentityProfileService>();
    }
}
