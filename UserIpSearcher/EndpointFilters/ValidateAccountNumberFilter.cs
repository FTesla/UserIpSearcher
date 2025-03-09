namespace UserIpSearcher.EndpointFilters;

/// <summary>
///     Validation of input data.
/// </summary>
public class ValidateAccountNumberFilter : IEndpointFilter
{
    /// <summary>
    ///     Method for embedding in middleware.
    /// </summary>
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        try
        {
            var accountNumber = context.GetArgument<long>(3);
        }
        catch (Exception e)
        {
            return TypedResults.Problem("isCompleted Error");
        }

        return await next(context);
    }
}
