namespace GameDataLibrary
{
    [System.Serializable]
    public class CharacterData
    {
        public int Id { get; set; }
        public int PlayerID;
        public string Name { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public ECharacterClass CharacterClass { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }

        // Constructor
        public CharacterData(int id, string name, int level, int experience, ECharacterClass characterClass, int health, int mana)
        {
            Id = id;
            Name = name;
            Level = level;
            Experience = experience;
            CharacterClass = characterClass;
            Health = health;
            Mana = mana;
        }

        public override string ToString()
        {
            return "Name: " + Name + "\nLevel: " + Level + "\nExp: " + Experience + "\nCharacter Class: " + CharacterClass + 
                "\nHealth: " + Health + "\nMana: " + Mana;
        }
    }
}
