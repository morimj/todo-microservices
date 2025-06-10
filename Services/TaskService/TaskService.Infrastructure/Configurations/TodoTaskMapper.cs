using TaskService.Domain.Entities;
using TaskService.Infrastructure.Persistence.Documents;

namespace TaskService.Infrastructure.Configurations
{
    public static class TodoTaskMapper
    {
        public static TodoTask ToEntity(this TodoTaskDocument doc)
        {
            if (doc == null) return null;

            var entity = new TodoTask(doc.TaskTitle, doc.UserId);

            entity.SetCreatedAt(doc.CreatedAt);
            entity.SetId(doc.Id);
            entity.SetUpdatedAt(doc.UpdatedAt);
            entity.SetIsCompleted(doc.IsCompleted);

            return entity;
        }

        public static TodoTaskDocument ToDocument(this TodoTask entity)
        {
            return new TodoTaskDocument
            {
                Id = entity.Id,
                TaskTitle = entity.TaskTitle,
                IsCompleted = entity.IsCompleted,
                UserId = entity.UserId,
                CreatedAt = entity.CreatedAt
            };
        }
    }
}
