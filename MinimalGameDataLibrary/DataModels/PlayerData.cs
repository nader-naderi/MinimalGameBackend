using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalGameDataLibrary
{
    public class PlayerData
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int Level { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string PlayerPosition { get; set; }
        public string CoinPosition { get; set; }
    }
}
