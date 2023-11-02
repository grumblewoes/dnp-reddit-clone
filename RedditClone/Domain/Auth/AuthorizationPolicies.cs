namespace Domain.Auth;

using Microsoft.Extensions.DependencyInjection;

public static class AuthorizationPolicies
{
    public static void AddPolicies(IServiceCollection services)
    {
        services.AddAuthorizationCore(options =>
            options.AddPolicy("MustBeUser", a => a.RequireAuthenticatedUser()));
    }
}