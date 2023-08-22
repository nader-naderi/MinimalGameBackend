namespace GameDataLibrary
{
    [System.Serializable]
    public class WeaponData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EWeaponType WeaponType { get; set; }
        public int Damage { get; set; }
        public int MagicDamage { get; set; }
        public int AttackSpeed { get; set; }
        public int Durability { get; set; }

        public WeaponData()
        {
            
        }

        public WeaponData(int id, string name, EWeaponType weaponType, int damage, int magicDamage, int attackSpeed, int durability)
        {
            Id = id;
            Name = name;
            WeaponType = weaponType;
            Damage = damage;
            MagicDamage = magicDamage;
            AttackSpeed = attackSpeed;
            Durability = durability;
        }
    }
}
