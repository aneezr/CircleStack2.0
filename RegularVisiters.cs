using System;
using System.Collections.Generic;
using System.Text;

namespace CircleStack
{
    public class RegularVisiter
    {
        public string Name;
        public int ID;
        public string Reason;
        public string AptNO;

        public RegularVisiter(string name, int id, string reason)
        {
            this.Name = name;
            this.ID = id;
            this.Reason = reason;
            //this.AptNO = apt_no;
        }
    }

}
