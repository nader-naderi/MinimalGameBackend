using Newtonsoft.Json;

namespace DataTransferObjects.DataTransferObjects.UserDTOs
{
    [JsonObject]
    public class UserLoginDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
