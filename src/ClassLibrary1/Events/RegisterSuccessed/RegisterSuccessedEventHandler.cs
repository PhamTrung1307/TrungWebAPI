using Core.Events.RegisterSuccessed;
using MediatR;


namespace TeduBlog.Core.Events.RegisterSuccessed
{
    internal class RegisterSuccessedEventHandler : INotificationHandler<RegisterSuccessedEvent>
    {
        public Task Handle(RegisterSuccessedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
