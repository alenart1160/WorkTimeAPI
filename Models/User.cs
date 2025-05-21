using Microsoft.AspNetCore.Identity;

namespace WorkTimeAPI.Model
{
    public class User
    {
        public long Id { get; set; }
        public required string Login {  get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public string? NIP { get; set; }
    }


}
