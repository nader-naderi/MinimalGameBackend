using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalGameDataLibrary
{
    public class GameData
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Player Navigation
        [ForeignKey(nameof(Player))]
        public int PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public PlayerData Player { get; set; }

        // User Navigation
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public UserData User { get; set; }
    }
}
