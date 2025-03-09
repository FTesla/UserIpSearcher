namespace UserIpSearcher.EndpointFilters;

/// <summary>
///     Validation of input data.
/// </summary>
public class ValidateIpFilter : IEndpointFilter
{
    /// <summary>
    ///     Method for embedding in middleware.
    /// </summary>
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        try
        {
            var ipAddress = context.GetArgument<string>(3);

            if(string.IsNullOrEmpty(ipAddress))
                return TypedResults.Problem("ipAddress is null or empty");

            if(ipAddress.Length > 39)
                return TypedResults.Problem("ipAddress is very long");
        }
        catch (Exception e)
        {
            return TypedResults.Problem("ipAddress Error");
        }

        return await next(context);
    }
}
