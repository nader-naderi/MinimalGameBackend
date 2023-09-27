using Newtonsoft.Json;

namespace DataTransferObjects.DataTransferObjects.UserDTOs
{
    [JsonObject]
    public class UserRegisterationDto : UserLoginDto
    {
        public string UserRole { get; set; } = "User";
    }
}
