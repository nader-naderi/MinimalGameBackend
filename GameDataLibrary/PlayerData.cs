using System;

namespace GameDataLibrary
{
    [Serializable]
    public class PlayerData
    {
        public int Id { get; set; }
        public UserData UserData { get; set; }
        public CharacterData CharacterData { get; set; }
    }
}
