namespace TaskService.Application.Publishers
{
    public interface ITaskCreatedPublisher
    {
        Task PublishAsync(Guid taskId, string title);
    }
}
