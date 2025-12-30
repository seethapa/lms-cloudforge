using ApplicationCore.DTO;
using ApplicationCore.Interfaces;
using ApplicationCore.Model;
using Infrastructure.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly MongoDbContext _db;

        public UserService(MongoDbContext db)
        {
            _db = db;
        }

        public async Task<User> Register(User user, string password)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            await _db.Users.InsertOneAsync(user);
            return user;
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _db.Users
                .Find(u => u.Email == email)
                .FirstOrDefaultAsync();
        }

        public async Task<User?> AddUser(RegisterUserRequest request)
        {
            var email = request.Email.Trim().ToLower();

            var existing = await _db.Users
                .Find(x => x.Email == email)
                .FirstOrDefaultAsync();

            if (existing != null)
                return null;

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = email,   
                FirstName = request.FirstName,
                LastName = request.LastName,
                Role = "student",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                CreatedAt = DateTime.UtcNow
            };

            try
            {
                await _db.Users.InsertOneAsync(user);
                return user;
            }
            catch (MongoWriteException ex) when
                (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
            {
                return null;
            }
        }

        public async Task<User?> GetById(string id)
        {
            return await _db.Users
                .Find(u => u.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<User> UpdateByEmail(string email, UpdateUserRequest request)
        {
            var normalizedEmail = email.Trim().ToLower();

            var update = Builders<User>.Update
                .Set(x => x.FirstName, request.FirstName)
                .Set(x => x.LastName, request.LastName);

            var result = await _db.Users.UpdateOneAsync(
                u => u.Email == normalizedEmail,
                update);

            if (result.MatchedCount == 0)
                throw new Exception("User not found");

            // 🔹 Return updated user
            return await _db.Users
                .Find(u => u.Email == normalizedEmail)
                .FirstAsync();
        }
    }
}
