using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Data
{
    public class UserDetails
    {
        public string Gender { get; set; }
        public string AgeRange { get; set; }
        public string Nationality { get; set; }

        public UserDetails(string gender, string ageRange, string nationality)
        {
            Gender = gender;
            AgeRange = ageRange;
            Nationality = nationality;
        }
    }
}
