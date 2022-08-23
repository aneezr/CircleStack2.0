using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CircleStack
{
    class FamilyMember
    {
        public string Name { get; set; }
        public string Occupation { get; set; }

        //DateTime dt = DateTime.ParseExact("24/01/2013", "dd/MM/yyyy", CultureInfo.InvariantCulture);
        public DateTime DOB { get; set; }

        public string Relation { get; set; }

        public FamilyMember(string name, string dob, string rel, string occupation)
        {

            this.Name = name;
            this.Occupation = occupation;
            if (DateTime.ParseExact(dob, "dd/MM/yyyy", CultureInfo.InvariantCulture) > DateTime.Now)
            {
                throw new Exception("Date of birth is invalid");
            }
            this.DOB = DateTime.ParseExact(dob, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            this.Relation = rel;
        }
    }
}
