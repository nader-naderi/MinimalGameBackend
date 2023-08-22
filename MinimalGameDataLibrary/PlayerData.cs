using System;
using System.ComponentModel.DataAnnotations;

namespace MinimalGameDataLibrary
{

    [Serializable]
    public class PlayerData
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Score must be a non-negative number.")]
        public int Score { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Level must be a positive number.")]
        public int Level { get; set; }

        [Display(Name = "Submission Date")]
        [DataType(DataType.DateTime)]
        public DateTime DateSubmitted { get; set; }

        public Vec3Data PlayerPosition { get; set; }
        public Vec3Data CoinPosition { get; set; }
    }
}
