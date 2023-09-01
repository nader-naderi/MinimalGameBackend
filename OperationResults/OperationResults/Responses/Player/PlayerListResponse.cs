using DataTransferObjects.DataTransferObjects;
using System.Collections.Generic;

namespace MinimalGameDataLibrary.OperationResults
{
    public class PlayerListResponse : OperationResult
    {
        public List<PlayerOutputDto> Players { get; set; } = new();
    }

}
