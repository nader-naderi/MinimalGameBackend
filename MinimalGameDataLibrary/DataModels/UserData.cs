using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalGameDataLibrary
{
    public class UserData
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string PasswordHash { get; set; }
        public string Role { get; set; }

        // Player Navigation
        [ForeignKey(nameof(Player))]
        public int PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public PlayerData Player { get; set; }
    }
}
