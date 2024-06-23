namespace Backend.Session;

public class SessionManager : ISessionProvider
{
    private static readonly HostString HostString = new("Valid host");
    public UserSession GetSession(Guid sessionId)
    {
        return new UserSession(sessionId)
        {
            ClientHost = HostString
        };
    }
}