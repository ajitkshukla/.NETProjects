namespace StudentAPI.Model
{
    public class Address
    {
        public int Id { get; set; }
        public int ZipCode { get; set; }
        public int BuildingNumber { get; set; }
        public string? AptNo { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
    }
}
