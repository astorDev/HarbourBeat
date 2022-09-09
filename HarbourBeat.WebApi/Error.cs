using Nist.Responses;

public record Error(HttpStatusCode Code, string Reason) : Nist.Errors.Error(Code, Reason)
{
    public static Error Unknown => new (HttpStatusCode.InternalServerError, "Unknown");
    public static Error UnknownDashboard => new(HttpStatusCode.BadRequest, "UnknownDashboard");
    
    public static Error Interpret(Exception exception)
    {
        return exception switch
        {
            UnknownDashboardException _ => UnknownDashboard,
            _ => Unknown
        };
    }
}

public class UnknownDashboardException : Exception
{
}

public class KibanaException : Exception
{
    public KibanaException(string message, Exception internalException) : base($"Kibana error : {message}", internalException)
    {
    }
    
    public static async Task Wrap(Func<Task> action)
    {
        try { await action(); } 
        catch (UnsuccessfulResponseException e) { throw new KibanaException(e.Body, e); }
    }
}

