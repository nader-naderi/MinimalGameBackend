using DataTransferObjects.DataTransferObjects;

namespace MinimalGameDataLibrary.OperationResults
{
    public class PlayerGetResponse : PlayerOperationResult
    {
        public PlayerOutputDto PlayerData { get; set; } = new PlayerOutputDto();
    }

}
