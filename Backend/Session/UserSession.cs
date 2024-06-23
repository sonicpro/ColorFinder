namespace Backend.Session;

public class UserSession
{
    public Guid SessionId { get; init; }
    public string ClientName { get; init; }
    public HostString ClientHost { get; init; }

    public List<string> PermittedCameras { get; init; }

    public UserSession(Guid sessionId)
    {
        SessionId = sessionId;
    }
}