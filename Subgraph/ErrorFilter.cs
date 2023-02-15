public class ErrorFilter : IErrorFilter
{
    private readonly ILogger<ErrorFilter> _log;

    public ErrorFilter(ILogger<ErrorFilter> log)
    {
        _log = log;
    }

    public IError OnError(IError error)
    {
        _log.LogError(error.Exception, "An error occurred.");

        return error.WithException(error.Exception);
    }
}