using UserIpSearcher.EndpointFilters;
using UserIpSearcher.EndpointHandlers;

namespace UserIpSearcher.Extensions;

/// <summary>
///     Extensions endpoint route builder.
/// </summary>
public static class EndpointRouteBuilderExtensions
{
    /// <summary>
    ///     Find endpoints.
    /// </summary>
    public static void RegisterSearchEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("/get-users-by-ip", SearchHandler.GetUsersByIpAsync)
            .AddEndpointFilter<ValidateIpFilter>()
            .WithName("GetUsersByIp")
            .WithOpenApi()
            .WithSummary("Find users by IP.")
            .WithDescription("Filters: 'Address' - partial or full IP address.");

        endpointRouteBuilder.MapGet("/get-ips-by-user", SearchHandler.GetIpsByUserAsync)
            .AddEndpointFilter<ValidateAccountNumberFilter>()
            .WithName("GetIpsByUser")
            .WithOpenApi()
            .WithSummary("Find IPs by User.")
            .WithDescription("Filters: 'AccountNumber' - user account number.");

        endpointRouteBuilder.MapGet("/get-date-connect-by-user", SearchHandler.GetLastDateConnectByUserAsync)
            .AddEndpointFilter<ValidateAccountNumberFilter>()
            .WithName("GetLastDateConnectByUser")
            .WithOpenApi()
            .WithSummary("Find last user connection.")
            .WithDescription("Filters: 'AccountNumber' - user account number.");
    }

    /// <summary>
    ///     User endpoint.
    /// </summary>
    public static void RegisterCreateUserIpDataEndpoint(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPost("/user-ip-data", UserIpEventHandler.CreateUserIpDataAsync)
            .WithName("CreateUserIpData")
            .WithOpenApi()
            .WithSummary("Simulating the operation of the event service! Read events to create user IP data.");
    }
}
