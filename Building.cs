using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace CircleStack
{
    class Building : IBuilding
    {
        string FlatName = "";
        int TotalAppartments;

        BuildingDetails Details;


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
                    this.Details.Unoccupied--;
                    Console.WriteLine("Apartment updated");
                }
                else
                {
                    throw new Exception("Please check !! Apartment number already exists and is occupied");
                }

            }
            else
            {
                if (!IsOccupied || owner_name.Equals("UNOCCUPIED") )
                {
                    owner_name = "UNOCCUPIED";
                    IsOccupied = false;
                    this.Details.Unoccupied++;
                }
                Apartment item = new Apartment(apt_no, owner_name, type, IsOccupied,this.Details);
                TotalApartmentList.Add(item);
                this.UpdateState();
            }
        }

        public void AddFamilyMember(string apt_no,string name, string dob, string rel, string occupation)
        {
            if(TotalApartmentList.Any(item => item.AptNo == apt_no))
            {
                var index = TotalApartmentList.FindIndex(a => a.AptNo == apt_no);
                TotalApartmentList[index].AddMembers(name, dob, rel, occupation);
                this.UpdateState();

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
                if (Details.TotalVehicleList.Any(item => item.LicensePlate.Equals(lp))){
                    throw new Exception("This Vehicle has been already added !!");
                }
                else
                {
                    var index = TotalApartmentList.FindIndex(a => a.AptNo == apt_no);
                    TotalApartmentList[index].AddVehicle(lp, type, model, isin);
                    this.UpdateState();
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
                this.UpdateState();
                Console.WriteLine($"{name} (visiter) added succesfully");
            }
        }

        public void RemoveVisitor(string name,string apt_no)
        {
            var index = TotalApartmentList.FindIndex(item => item.AptNo == apt_no);
            TotalApartmentList[index].RemoveVisitor(name,apt_no);
            this.UpdateState();
        }

        public void GetReport()
        {
            Console.WriteLine("\nFlat Report");
            Console.WriteLine($"Number of unoccupied flats : {this.Details.TotalVehicleList.Count}");
            Console.WriteLine($"Total occupants in the flat : {this.Details.TotalMembersList.Count}");
            Console.WriteLine($"Total number of vehicles : {this.Details.TotalVehicleList.Count}");
            Console.WriteLine($"Total vehicles inside the apartment : {this.Details.TotalVehicleList.Where(p => p.IsIn == true).ToList().Count}");
            Console.WriteLine($"Total apartments added : {this.TotalApartmentList.Count}");
        }

        void UpdateState()
        {
            List<string> State = new List<string>();
            foreach (Apartment apartment in TotalApartmentList)
            {
                State.Add($"AD,{apartment.AptNo},{apartment.OwnerName},{apartment.Type}");
                if (apartment.IsOccupied)
                {
                    foreach (FamilyMember FM in apartment.Members)
                    {
                        State.Add($"FM,{FM.Name},{FM.Occupation},{FM.DOB},{FM.Relation}");
                    }
                    foreach (Vehicle Vehicle in apartment.Vehicles)
                    {
                        State.Add($"VD,{Vehicle.LicensePlate},{Vehicle.Type},{Vehicle.Model},{(Vehicle.IsIn ? "IN" : "OUT")}");
                    }
                    foreach (RegularVisiter item in apartment.RegularVisiters)
                    {
                        State.Add($"RV,{item.ID},{item.Name},{item.Reason}");
                    }
                }
            }
            File.AppendAllLines("state.txt", State);

        }

        public void UpdateVehicleStatus(string apt_no, string lp,string status)
        {
            if(!Details.TotalVehicleList.Any(item => item.LicensePlate.Equals(lp)))
            {
                throw new Exception("-----Vehicle doesn't exists------");
            }else if(!TotalApartmentList.Any(item => item.AptNo == apt_no))
            {
                throw new Exception("------Apartment doesn't exist------");
            }
            else
            {
                Details.UpdateVehicleStatus(apt_no,lp,status);
            }
        }

        public void MigrateData(string path)
        {
            Console.WriteLine("xxxxxxxx Imgrating your data xxxxxxxxxx");
            var values = File.ReadAllLines(path);
            string cur_apt = "";
            foreach (string i in values)
            {
                var fields = i.Split(",");
                switch (fields[0])
                {
                    case "AD":
                        if (fields[2].Equals("UNOCCUPIED"))
                        {
                            this.AddAppartment(fields[1], fields[2], fields[3], false);
                        }
                        else
                        {
                            cur_apt = fields[1];
                            //Console.WriteLine(cur_apt);
                            this.AddAppartment(fields[1], fields[2], fields[3], true);
                        }
                        break;

                    case "FM":
                        this.AddFamilyMember(cur_apt, fields[1], fields[3], fields[4], fields[2]);
                        break;

                    case "VD":
                        if (fields[4].Equals("IN"))
                        {
                            this.AddAVehicle(fields[1], fields[2], fields[3], true,cur_apt);
                        }
                        else
                        {
                            this.AddAVehicle(fields[1], fields[2], fields[3], false,cur_apt);
                        }
                        break;

                    case "RV":
                        this.AddAVisitor(fields[2], Convert.ToInt32(fields[1]), fields[3], cur_apt);
                        break;
                }
            }
            Console.WriteLine("\nxxxxxxxx Transfer Succesfull xxxxxxxxxx");
        }
    }
}
