using DataTransferObjects.DataTransferObjects.PlayerDTOs;

namespace MinimalGameDataLibrary.OperationResults
{
    public class PlayerGetResponse : PlayerOperationResult
    {
        public PlayerOutputDto PlayerData { get; set; } = new PlayerOutputDto();
    }

}
