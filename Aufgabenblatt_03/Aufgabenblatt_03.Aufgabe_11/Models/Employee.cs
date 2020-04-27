namespace Aufgabenblatt_03.Aufgabe_11.Models
{
    public class Employee
    {
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public Address Address { get; set; } = new Address();


    }
}
