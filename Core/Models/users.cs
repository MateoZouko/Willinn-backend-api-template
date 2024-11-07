namespace Core.Models
{
public class Users
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required bool IsActive { get; set; }
    }
}