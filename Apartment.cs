using System;
using System.Collections.Generic;
using System.Text;

namespace CircleStack
{
    class Apartment : IApartment
    {

        public string AptNo { get; set; }
        public string OwnerName { get; set; }
        public string Type { get; set; }
        public bool IsOccupied { get; set; }
        public int MaxOccupancy { get; set; }
        public BuildingDetails Details { get; set; }


        public List<FamilyMember> Members = new List<FamilyMember>();
        public List<Vehicle> Vehicles = new List<Vehicle>(); 
        public List<RegularVisiter> RegularVisiters = new List<RegularVisiter>();


        public Apartment(string apt_no, string owner_name, string type, bool IsOccupied, BuildingDetails details)
        {
            this.AptNo = apt_no;
            this.OwnerName = owner_name;
            this.Type = type;
            if (type.Equals("3BHK"))
            {
                this.MaxOccupancy = 6;
            }else if (type.Equals("2BHK"))
            {
                this.MaxOccupancy = 5;
            }else if (type.Equals("Studio"))
            {
                this.MaxOccupancy = 2;
            }
            this.IsOccupied = IsOccupied;
            this.Details = details;
        }

        public void AddMembers(string name, string dob, string rel, string occupation)
        {
            var fm = new FamilyMember(name, dob, rel, occupation);
            this.Members.Add(fm);
            this.Details.TotalMembersList.Add(fm);
        }

        public void AddVehicle(string lp, string type, string model, bool isin)
        {
            var v = new Vehicle(lp, type, model, isin);
            this.Vehicles.Add(v);
            this.Details.TotalVehicleList.Add(v);
        }

        public void AddRegularVisiter(string name, int id, string reason)
        {
            var rv = new RegularVisiter(name, id, reason);
            this.RegularVisiters.Add(rv);
            this.Details.TotalVisitersList.Add(rv);
        }

        public void RemoveVisitor(string name, string apt_no)
        {
            RegularVisiters.RemoveAll(x => x.Name == name);
            this.Details.TotalVisitersList.RemoveAll(x => x.Name == name);
        }
    }
}
