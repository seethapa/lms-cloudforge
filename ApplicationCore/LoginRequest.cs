using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.DTO
{
    public class LoginRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
