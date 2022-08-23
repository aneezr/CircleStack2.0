using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CircleStack
{
    class BuildingDetails : IBuildingDetails
    {
        public int Unoccupied;
        public int CurrentVehiclesCount;

        public List<RegularVisiter> TotalVisitersList = new List<RegularVisiter>();
        public List<FamilyMember> TotalMembersList = new List<FamilyMember>();
        public List<Vehicle> TotalVehicleList = new List<Vehicle>();

        public BuildingDetails()
        {
            this.Unoccupied = 0;
        }

        public void UpdateVehicleStatus(string apt_no, string lp, string status)
        {
            if (status.Equals("in"))
            {
                this.TotalVehicleList[this.TotalVehicleList.FindIndex(a => a.LicensePlate.Equals(lp))].IsIn = true;

            }
            else
            {
                this.TotalVehicleList[this.TotalVehicleList.FindIndex(a => a.LicensePlate.Equals(lp))].IsIn = false;
            }
            this.CurrentVehiclesCount = this.TotalVehicleList.Where(p => p.IsIn == true).ToList().Count;
        }
    }
}
