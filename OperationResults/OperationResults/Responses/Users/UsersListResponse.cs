using DataTransferObjects.DataTransferObjects.UserDTOs;

namespace MinimalGameDataLibrary.OperationResults
{
    public class UsersListResponse : OperationResult
    {
        public List<UserRegisterationDto> Users { get; set; } = new();
    }
}
