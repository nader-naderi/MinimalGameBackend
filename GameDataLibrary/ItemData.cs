namespace GameDataLibrary
{
    [System.Serializable]
    public class ItemData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EItemType ItemType { get; set; }
        public int Weight { get; set; }
        public string Description { get; set; }

        public ItemData()
        {
            
        }

        // Constructor
        public ItemData(int id, string name, EItemType itemType, int weight, string description)
        {
            Id = id;
            Name = name;
            ItemType = itemType;
            Weight = weight;
            Description = description;
        }
    }
}
