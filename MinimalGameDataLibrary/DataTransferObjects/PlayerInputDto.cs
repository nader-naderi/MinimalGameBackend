using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalGameDataLibrary.DataTransferObjects
{
    public class PlayerInputDto
    {
        public string Name { get; set; }    
        public int Level { get; set; }
        public int Score { get; set; }
        public DateTime DateSubmitted;
        public Vec3Data PlayerPosition { get; set; }
        public Vec3Data CoinPosition{ get; set; }
    }

    public class PlayerOutputDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Score { get; set; }
        public DateTime DateSubmitted;
        public Vec3Data PlayerPosition { get; set; }
        public Vec3Data CoinPosition { get; set; }
    }
}
