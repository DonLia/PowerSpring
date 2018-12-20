using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using PowerSpring.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PowerSpring.Helper
{
    public class UserManager : IUserManager
    {
        private AppDbContext _context;

        public UserManager(AppDbContext context)
        {
            _context = context;
        }

        public WebUser Authenticate(string UserName, string password)
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.WebUsers.FirstOrDefault(x => x.UserName == UserName);

            // check if UserName exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        public IEnumerable<WebUser> GetAll()
        {
            return _context.WebUsers;
        }

        public WebUser GetById(int id)
        {
            return _context.WebUsers.Find(id);
        }

        public WebUser Create(string username, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_context.WebUsers.Any(x => x.UserName == username))
                throw new AppException("UserName \"" + username + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = new WebUser();
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.UserName = username;
            _context.WebUsers.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Update(WebUser userParam, string password = null)
        {
            var user = _context.WebUsers.Find(userParam.Id);

            if (user == null)
                throw new AppException("User not found");

            if (userParam.UserName != user.UserName)
            {
                // UserName has changed so check if the new UserName is already taken
                if (_context.WebUsers.Any(x => x.UserName == userParam.UserName))
                    throw new AppException("UserName " + userParam.UserName + " is already taken");
            }

            // update user properties
            user.Email = userParam.Email;
            user.Phone = userParam.Phone;
            user.UserName = userParam.UserName;

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.WebUsers.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.WebUsers.Find(id);
            if (user != null)
            {
                _context.WebUsers.Remove(user);
                _context.SaveChanges();
            }
        }

        // private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
