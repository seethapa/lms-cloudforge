using ApplicationCore.DTO;
using ApplicationCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IUserService
    {
        Task<User> Register(User user, string password);
        Task<User?> GetByEmail(string email);
        Task<User> AddUser(RegisterUserRequest request);
        Task<User?> GetById(string id);
        Task<User> UpdateByEmail(string email, UpdateUserRequest request);
    }
}
