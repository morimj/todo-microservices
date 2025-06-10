namespace SharedKernel.Events
{
    public record TaskCreated(
        Guid TaskId,
        string Title,
        DateTime CreatedAt,
        Guid CreatedByUserId
        );
}
