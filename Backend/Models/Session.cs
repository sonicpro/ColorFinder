namespace Backend.Models;

internal class Session
{
    public Guid SessionId { get; set; }
    public HostString ClientHost { get; set; }
}