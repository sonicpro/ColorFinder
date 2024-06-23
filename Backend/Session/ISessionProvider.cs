using System;

namespace Backend.Session;

public interface ISessionProvider
{
    UserSession GetSession(Guid sessionId);
}