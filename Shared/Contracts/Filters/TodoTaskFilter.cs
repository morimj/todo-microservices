namespace SharedKernel.Filters
{
    public class TodoTaskFilter
    {
        public Guid? UserId { get; set; }
        public bool? IsCompleted { get; set; }
        public string? TitleContains { get; set; }
    }
}