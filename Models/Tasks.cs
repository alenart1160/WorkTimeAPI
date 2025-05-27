using System.Numerics;

namespace WorkTimeAPI.Models
{
    public class Tasks
    {
        public long Id { get; set; }
        public long UserID { get; set; }
        public string? Title { get; set; }
        public DateTime DueDate { get; set; }
        public int Priority { get; set; }
        public string? Description { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public EnumStatus Status { get; set; }
        public string? TimePassed { get; set; }
        public bool Completed { get; set; }


        public enum EnumStatus
        {
            InComplete = 0,
            Active = 1,
            Completed = 2,
        }

    }
}
