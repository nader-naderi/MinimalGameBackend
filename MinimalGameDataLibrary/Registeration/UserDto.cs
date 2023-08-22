using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalGameDataLibrary.Registeration
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }
    }

    public class OperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class RegisterationResponse : OperationResult
    {
        public string ErrorCode { get; set; }
    }

    public class LoginResponse : OperationResult
    {
        public string Token { get; set; }
        public string ErrorCode { get; set; }
    }
}
