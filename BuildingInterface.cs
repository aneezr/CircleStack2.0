using System;
using System.Collections.Generic;
using System.Text;

namespace CircleStack
{
    interface IBuilding
    {
        void AddAppartment(string apt_no, string owner_name, string type, bool IsOccupied);
        void AddFamilyMember(string apt_no, string name, string dob, string rel, string occupation="none");
        void AddAVehicle(string lp, string type, string model, bool isin, string apt_no);
        void AddAVisitor(string name, int id, string reason, string apt_no);
        void RemoveVisitor(string name, string apt_no);
        void UpdateVehicleStatus(string apt_no, string lp, string status);
        void GetReport();
        void MigrateData(string path);
    }
}
