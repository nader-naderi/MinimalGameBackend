using DataTransferObjects.DataTransferObjects.PlayerDTOs;

using Newtonsoft.Json;

namespace DataTransferObjects.DataTransferObjects.UserDTOs
{
    [JsonObject]
    public class UserDto : UserRegisterationDto
    {
        public PlayerOutputDto? Player { get; set; }
    }
}
