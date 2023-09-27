using Newtonsoft.Json;

namespace DataTransferObjects.DataTransferObjects.PlayerDTOs
{
    [JsonObject]
    public class PlayerOutputDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Level { get; set; }
        public int Score { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string PlayerPosition { get; set; } = string.Empty;
        public string CoinPosition { get; set; } = string.Empty;
    }
}
