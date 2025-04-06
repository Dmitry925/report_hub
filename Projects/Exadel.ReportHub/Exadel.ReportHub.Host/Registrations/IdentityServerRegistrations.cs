using Duende.IdentityServer.Services;
using Exadel.ReportHub.Identity;
using Exadel.ReportHub.Identity.Stores;

namespace Exadel.ReportHub.Host.Registrations;

public static class IdentityServerRegistrations
{
    public static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentityServer(options =>
        {
            options.Events.RaiseErrorEvents = true;
            options.Events.RaiseInformationEvents = true;
            options.Events.RaiseFailureEvents = true;
            options.Events.RaiseSuccessEvents = true;
        })
            .AddClientStore<IdentityClientStore>()
            .AddResourceStore<IdentityResourceStore>()
            .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
            .AddProfileService<IdentityProfileService>()
            .AddDeveloperSigningCredential(false);

        services.AddScoped<IProfileService, IdentityProfileService>();
    }
}
