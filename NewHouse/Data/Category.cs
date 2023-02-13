namespace NewHouse.Data
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public int TypeId { get; set; }
        public Type Types { get; set; }

    }
}
