namespace NewHouse.Data
{
    public class Type
    {
        public int Id { get; set; }
        public string TypeName { get; set; }

        public ICollection<Property> Properties { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
