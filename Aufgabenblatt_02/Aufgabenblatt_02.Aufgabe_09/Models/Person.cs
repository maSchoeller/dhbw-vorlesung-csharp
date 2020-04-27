using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabenblatt_02.Aufgabe_09.Models
{
    public class Person
    {
        public event Action<string> NameChanged;

        private string _firstname = string.Empty;
        public string Firstname
        {
            get => _firstname;
            set
            {
                _firstname = value;
                NameChanged?.Invoke(value);
            }
        }

        private string _lastname = string.Empty;
        public string Lastname
        {
            get => _lastname;
            set
            {
                _lastname = value;
                NameChanged?.Invoke(value);
            }
        }




    }
}
