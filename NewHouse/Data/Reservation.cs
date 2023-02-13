namespace NewHouse.Data
{
    public class Reservation
    {
        public int Id { get; set; }
        public int Period { get; set; }
        public DateTime RegisterOn { get; set; }

        public int PropertyId { get; set; }
        public Property Properties { get; set; }

        public string UserId { get; set; }

        public User Users { get; set; }
    }
}
