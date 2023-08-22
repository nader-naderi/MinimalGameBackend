namespace GameDataLibrary
{
    [System.Serializable]
    public class EquipmentData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EEquipmentSlot EquipmentSlot { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int MagicAttack { get; set; }
        public int MagicDefense { get; set; }
        // Add more properties as needed

        public EquipmentData()
        {
            
        }

        // Constructor
        public EquipmentData(int id, string name, EEquipmentSlot equipmentSlot, int attack, int defense, int magicAttack, int magicDefense)
        {
            Id = id;
            Name = name;
            EquipmentSlot = equipmentSlot;
            Attack = attack;
            Defense = defense;
            MagicAttack = magicAttack;
            MagicDefense = magicDefense;
        }
    }
}
