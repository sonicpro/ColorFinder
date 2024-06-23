using Backend.Session;
using FluentValidation;

namespace Backend.Validators;

internal class SessionValidator : AbstractValidator<Models.Session>
{
    public SessionValidator(ISessionProvider sessionProvider)
    {
        RuleFor(s => s.SessionId).NotNull().NotEmpty().WithMessage("Empty session id.");
        RuleFor(s => s.ClientHost).NotNull().NotEmpty().WithMessage("Undefined client host.");

        RuleFor(s => s)
            .Must(s => sessionProvider.GetSession(s.SessionId) != null)
            .WithMessage("Session ID is not valid.")
            .Must(s => sessionProvider.GetSession(s.SessionId) is var session && session?.ClientHost == s.ClientHost)
            .WithMessage("User has already authorized on another host.");

    }
}