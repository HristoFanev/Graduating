namespace NewHouse.Data
{
    public enum StatusType { busy, free } //busy = 1, free = 2
    public class Property
    {
        public int Id { get; set; }
        public int Floor { get; set; }
        public double Quadrature { get; set; }
        public StatusType Status { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime RegisterOn { get; set; }
        public int TypeId { get; set; }
        public Type Type { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
