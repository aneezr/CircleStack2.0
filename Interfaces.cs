using System;
using System.Collections.Generic;
using System.Text;

namespace CircleStack
{
    interface IApartment
    {
        public void AddMembers(string name, string dob, string rel, string occupation);
        public void AddVehicle(string lp, string type, string model, bool isin);
        public void AddRegularVisiter(string name, int id, string reason);
        public void RemoveVisitor(string name, string apt_no);
    }

    interface IBuildingDetails
    {
        public void UpdateVehicleStatus(string apt_no, string lp, string status);
    }
}
