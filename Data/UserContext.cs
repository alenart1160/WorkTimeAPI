using Microsoft.EntityFrameworkCore;
using WorkTimeAPI.Model;
using WorkTimeAPI.Models;

namespace WorkTimeAPI.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Tasks> Tasks { get; set; } = null!;
        public DbSet<TaskTimer> TaskTimers { get; set; } = null!;
    }
}
