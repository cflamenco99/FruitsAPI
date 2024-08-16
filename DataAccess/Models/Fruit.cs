namespace DataAccess.Models
{
    public class Fruit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }  // FK FruitType
        public string Description { get; set; }

        public FruitType Type { get; set; }
    }
}
