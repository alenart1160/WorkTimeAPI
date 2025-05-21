namespace WorkTimeAPI.Models
{
    public class TaskTimer
    {
        public long Id { get; set; }
        public long UserID { get; set; }
        public long TaskID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public float TimePassed { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }
}