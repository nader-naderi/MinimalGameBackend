using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace MinimalGameDataLibrary
{
    [Serializable]
    public class UserData
    {
        public int Id { get; set; }
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string PasswordHash { get; set; }
        public string Role { get; set; }
    }
}
