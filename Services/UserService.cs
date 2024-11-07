using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Data;

namespace Services
{
    public class UserService
    {
        private readonly MyDbContext _context;

        public UserService(MyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Users> GetAllUsers() => _context.Users.ToList();

        public Users GetUserById(int id) => _context.Users.Find(id);

        public bool CreateUser(Users newUser)
        {
            if (_context.Users.Any(u => u.Id == newUser.Id))
                return false;

            _context.Users.Add(newUser);
            _context.SaveChanges();
            return true;
        }

        public bool Authenticate(string email, string password)
        {
            return _context.Users.Any(u => u.Email == email && u.PasswordHash == password);
        }

        public bool UpdateUser(int id, Users updatedUser)
        {
            var user = _context.Users.Find(id);
            if (user == null)
                return false;

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            user.PasswordHash = updatedUser.PasswordHash;
            user.IsActive = updatedUser.IsActive;
            _context.SaveChanges();
            return true;
        }

        public bool SoftDeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
                return false;

            user.IsActive = false;
            _context.SaveChanges();
            return true;
        }
    }
}
