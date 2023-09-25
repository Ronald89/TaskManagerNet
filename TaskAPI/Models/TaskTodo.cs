using System.ComponentModel.DataAnnotations;

namespace TaskAPI.Models
{
    public class TaskTodo
    {
        [Key]
        public int TaskId { get; set; }
        public required string UserId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
