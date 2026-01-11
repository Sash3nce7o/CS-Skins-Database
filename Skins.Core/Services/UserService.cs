using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using Skins.Core.Exceptions;
using Skins.Core.Models.User;
using Skins.Infrastructure.Common;
using Skins.Infrastructure.Data.Models;

namespace Skins.Core.Services
{
    public class UserService
    {
        private readonly IRepository _repo;

        public UserService(IRepository repo)
        {
            _repo = repo;
        }

        public void Add(UserRegisterViewModel entity)
{
    var existingUsers = _repo.AllAsNoTracking<User>()
        .Where(u => u.Email == entity.Email || u.Username == entity.Username)
        .ToList();
    
    if (existingUsers.Count > 0)
    {
        throw new InvalidOperationException("Email or Username already exists.");
    }
    
    var user = new User()
    {
        Username = entity.Username,
        Email = entity.Email,
        PasswordHash = HashPassword(entity.Password)  // Hash the password!
    };
    
        _repo.Add(user);
        _repo.SaveChanges();
    }



        public User GetById(string id)
        {
            if(string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id)){
                throw new NullReferenceException("Id cannot be null or whitespace.");
            }
            User user = _repo.GetById<User>(id);
            if (user == null){
                throw new NullReferenceException("User not found.");
            }
            return user;
        }
        public bool Remove(string id)
        {
            try
            {
                _repo.Delete<User>(id);
                _repo.SaveChanges();
                return true;
            }
            catch (NullReferenceException)
            {
                
                return false;
            }
        }

        public void Update(string id, UserUpdateViewModel model)
        {
            if(string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
            {
                throw new NotFoundException("ID cannot be null or empty");
            }
            User user = _repo.GetById<User>(id);
            if (user == null)
            {
                throw new NotFoundException($"user with Id {id} not found");
            }

            if (!string.IsNullOrEmpty(model.Username))
            {
                user.Username = model.Username;
            }
            if (!string.IsNullOrEmpty(model.Email))
            {
                user.Email = model.Email;
            }
            if (!string.IsNullOrEmpty(model.Password))
            {
                user.PasswordHash = HashPassword(model.Password);
            }

            _repo.Update(user);
            _repo.SaveChanges();
        }


        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string username, string password)
        {
            var user = _repo.AllAsNoTracking<User>()
                .FirstOrDefault(u => u.Username == username);
    
            if (user == null)
                return false;
    
            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        }

        public User? GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
                return null;
            
            return _repo.AllAsNoTracking<User>()
                .FirstOrDefault(u => u.Email == email);
        }
    }
}