using System;
using System.Collections.Generic;
using System.Text;

namespace CircleStack
{
    class Vehicle
    {
        public string LicensePlate { get; set; }
        public string Type { get; set; }
        public string Model { get; set; }
        public bool IsIn { get; set; }

        public Vehicle(string lp, string type, string model, bool isin)
        {
            this.LicensePlate = lp;
            this.Type = type;
            this.Model = model;
            this.IsIn = isin;
        }
    }
}
