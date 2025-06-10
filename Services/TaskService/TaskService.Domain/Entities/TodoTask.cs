using System.ComponentModel.DataAnnotations;

namespace TaskService.Domain.Entities
{
    public class TodoTask : BaseEntity
    {
        [MaxLength(150)]
        public string TaskTitle { get; private set; }
        public bool IsCompleted { get; private set; }
        public Guid UserId { get; private set; }

        private TodoTask() { }
        public TodoTask(string title, Guid userId)
        {
            SetTitle(title);
            SetUserId(userId);
        }

        public void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.");

            TaskTitle = title;
        }

        public void SetUserId(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("UserId cannot be empty.");

            UserId = userId;
        }

        public void SetIsCompleted(bool isCompleted)
        {
            IsCompleted = isCompleted;
        }

        public void MarkAsCompleted() => IsCompleted = true;
        public void MarkAsUnCompleted() => IsCompleted = false;
        public void ToggleCompleted() => IsCompleted = !IsCompleted;
    }
}