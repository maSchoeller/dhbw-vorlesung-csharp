namespace Aufgabenblatt_05.Aufgabe_13.Models
{
    public class Customer
    {
        public int Id { get; set; } 
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public Address Address { get; set; } = new Address();

    }
}
