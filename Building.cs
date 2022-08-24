using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using FileManager;


namespace CircleStack
{
    class Building : IBuilding
    {
        string FlatName = "";
        int TotalAppartments;

        BuildingDetails Details;

        FilereadManager FManager;

        WriteManager WM = new WriteManager();

        List<Apartment> TotalApartmentList = new List<Apartment>();

        public Building(string name, int total)
        {
            this.Details = new BuildingDetails();
            this.FlatName = name;
            this.TotalAppartments = total;
        }

        public void AddAppartment(string apt_no, string owner_name, string type, bool IsOccupied)
        {
            if (TotalApartmentList.Any(item => item.AptNo == apt_no))
            {
                int index = TotalApartmentList.FindIndex(item => item.AptNo == apt_no);
                if (!TotalApartmentList[index].IsOccupied)
                {
                    TotalApartmentList[index].OwnerName = owner_name;
                    TotalApartmentList[index].IsOccupied = true;

                    List<string> APList = new List<string>();

                    APList.Add($"AD,{TotalApartmentList[index].AptNo},{TotalApartmentList[index].OwnerName},{TotalApartmentList[index].Type}");
                   

                    this.Details.Unoccupied--;
                    WM.updateApartmentState(apt_no, APList);
                    Console.WriteLine("Apartment updated");
                }
                else
                {
                    throw new Exception("Please check !! Apartment number already exists and is occupied");
                }

            }
            else
            {
                if (!IsOccupied || owner_name.Equals("UNOCCUPIED"))
                {
                    owner_name = "UNOCCUPIED";
                    IsOccupied = false;
                    this.Details.Unoccupied++;
                }
                Apartment item = new Apartment(apt_no, owner_name, type, IsOccupied, this.Details);
                TotalApartmentList.Add(item);
                //this.UpdateState();
                WM.writeApartmentData(apt_no, $"{apt_no},{owner_name},{type}");
            }
        }

        public void AddFamilyMember(string apt_no, string name, string dob, string rel, string occupation)
        {
            if (TotalApartmentList.Any(item => item.AptNo == apt_no))
            {
                var index = TotalApartmentList.FindIndex(a => a.AptNo == apt_no);
                TotalApartmentList[index].AddMembers(name, dob, rel, occupation);
                WM.writeFamilyData(apt_no, $"{name},{occupation},{dob},{rel}");
            }
            else
            {
                throw new Exception("------Apartment doesn't exist------");
            }
        }

        public void AddAVehicle(string lp, string type, string model, bool isin, string apt_no)
        {
            if (TotalApartmentList.Any(item => item.AptNo == apt_no))
            {
                if (Details.TotalVehicleList.Any(item => item.LicensePlate.Equals(lp)))
                {
                    throw new Exception("This Vehicle has been already added !!");
                }
                else
                {
                    var index = TotalApartmentList.FindIndex(a => a.AptNo == apt_no);
                    TotalApartmentList[index].AddVehicle(lp, type, model, isin);
                    WM.writeFamilyData(apt_no, $"{lp},{type},{model},{(isin ? "IN" : "OUT")}");
                }
            }
            else
            {
                throw new Exception("------Apartment doesn't exist------");
            }
        }

        public void AddAVisitor(string name, int id, string reason, string apt_no)
        {
            if (Details.TotalVisitersList.Any(item => item.ID == id))
            {
                throw new Exception("Visitor already exists !!");
            }
            else if (!TotalApartmentList.Any(item => item.AptNo == apt_no))
            {
                throw new Exception("Apartment doesn't exist");
            }
            else
            {
                TotalApartmentList[TotalApartmentList.FindIndex(item => item.AptNo == apt_no)].AddRegularVisiter(name, id, reason);
                WM.writeVisitorData(apt_no, $"{id},{name},{reason}");
                Console.WriteLine($"{name} (visiter) added succesfully");
            }
        }

        public void RemoveVisitor(string name, string apt_no)
        {
            var index = TotalApartmentList.FindIndex(item => item.AptNo == apt_no);
            TotalApartmentList[index].RemoveVisitor(name, apt_no);
            List<string> RVList = new List<string>();
            foreach (var item in TotalApartmentList[index].RegularVisiters)
            {
                RVList.Add($"RV,{item.ID},{item.Name},{item.Reason}");
            }
            WM.updateVisitorState(apt_no,RVList);
            //this.UpdateState();
        }

        public void GetReport()
        {

            Console.WriteLine("\nFlat Report");
            Console.WriteLine($"Number of unoccupied flats : {Details.Unoccupied}");
            Console.WriteLine($"Total occupants in the flat : {Details.TotalMembersList.Count}");
            Console.WriteLine($"Total number of vehicles : {Details.TotalVehicleList.Count}");
            Console.WriteLine($"Total vehicles inside the apartment : {Details.TotalVehicleList.Where(p => p.IsIn == true).ToList().Count}");
            Console.WriteLine($"Total apartments added : {TotalApartmentList.Count}");
        }

        //public void UpdateState()
        //{
           

        //        List<string> State = new List<string>();
        //        foreach (Apartment apartment in TotalApartmentList)
        //        {
        //            WM.writeApartmentData(apartment.AptNo, $"{apartment.AptNo},{apartment.OwnerName},{apartment.Type},{apartment.IsOccupied}");
        //            if (apartment.IsOccupied)
        //            {
        //                foreach (FamilyMember FM in apartment.Members)
        //                {
        //                    //State.Add($"FM{FM.Name},{FM.Occupation},{FM.DOB},{FM.Relation}");
        //                    WM.writeFamilyData(apartment.AptNo, $"{FM.Name},{FM.Occupation},{FM.DOB},{FM.Relation}");
        //                }
        //                foreach (Vehicle Vehicle in apartment.Vehicles)
        //                {
        //                    //State.Add($"VD,{Vehicle.LicensePlate},{Vehicle.Type},{Vehicle.Model},{(Vehicle.IsIn ? "IN" : "OUT")}");
        //                    WM.writeVehicleData(apartment.AptNo, $"{Vehicle.LicensePlate},{Vehicle.Type},{Vehicle.Model},{(Vehicle.IsIn ? "IN" : "OUT")}");
        //                }
        //                foreach (RegularVisiter item in apartment.RegularVisiters)
        //                {
        //                    //State.Add($"RV,{item.ID},{item.Name},{item.Reason}");
        //                    WM.writeVisitorData(apartment.AptNo, $"{item.ID},{item.Name},{item.Reason}");
        //                }
        //            }
        //        }
        //        File.WriteAllLines("state.txt", State);
            
        //}

        public void UpdateVehicleStatus(string apt_no, string lp, string status)
        {
            if (!Details.TotalVehicleList.Any(item => item.LicensePlate.Equals(lp)))
            {
                throw new Exception("-----Vehicle doesn't exists------");
            }
            else if (!TotalApartmentList.Any(item => item.AptNo == apt_no))
            {
                throw new Exception("------Apartment doesn't exist------");
            }
            else
            {

                Details.UpdateVehicleStatus(apt_no, lp, status);

                var index = TotalApartmentList.FindIndex(item => item.AptNo == apt_no);

                List<string> VList = new List<string>();

                foreach (var item in TotalApartmentList[index].Vehicles)
                {
                    VList.Add($"VD,{item.LicensePlate},{item.Model},{(item.IsIn ? "IN" : "OUT")}");
                }
                WM.updateVehicleState(apt_no, VList);
            }
        }

        public void MigrateData(string path)
        {
            Console.WriteLine("xxxxxxxx Imgrating your data xxxxxxxxxx");
            List<APVO> AP = FManager.readAllData();
            foreach (APVO i in AP)
            {
                if (i.AD.ownerName.Equals("UNOCCUPIED"))
                {
                    this.AddAppartment(i.AD.apartmentNumber, i.AD.ownerName, i.AD.apartmentType, false);
                }
                else
                {
                    this.AddAppartment(i.AD.apartmentNumber, i.AD.ownerName, i.AD.apartmentType, true);
                }
                foreach (FMVO j in i.FMList)
                {
                    this.AddFamilyMember(i.AD.apartmentNumber, j.name, j.dob, j.relation, j.occupation);
                }
                foreach (RVVO j in i.RVList)
                {
                    this.AddAVisitor(j.name, Convert.ToInt32(j.visitorId), j.reason, i.AD.apartmentNumber);
                }
                foreach (VDVO j in i.VDList)
                {
                    this.AddAVehicle(j.licensePlateNumber, j.description, j.model, j.status.Equals("IN") ? true : false, i.AD.apartmentNumber);
                }
            }
            Console.WriteLine("\nxxxxxxxx Transfer Succesfull xxxxxxxxxx");
        }
    }
}
