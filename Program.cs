using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace CircleStack
{
    class Program
    {
        static void Main(string[] args)
        {
            Building Condoor = new Building("Condoor",20);
            try
            {
            var values = File.ReadAllLines(@"D:\Assessments\CircleStack\CircleStack\ams.txt");
                string cur_apt = "";
                foreach (string i in values)
                {
                    var fields = i.Split(",");
                    switch (fields[0])
                    {
                        case "AD":
                            if (fields[2].Equals("UNOCCUPIED"))
                            {
                                Condoor.AddAppartment(fields[1], fields[2], fields[3], false);
                            }
                            else
                            {
                                cur_apt = fields[1];
                                //Console.WriteLine(cur_apt);
                                Condoor.AddAppartment(fields[1], fields[2], fields[3], true);
                            }
                            break;

                        case "FM":
                            Condoor.AddFamilyMember(cur_apt, fields[1],fields[3], fields[4], fields[2]);
                            break;

                        case "VD":
                            if (fields[4].Equals("IN"))
                            {
                                Condoor.AddAVehicle(fields[1], fields[2], fields[3], true,cur_apt);
                            }
                            else
                            {
                                Condoor.AddAVehicle(fields[1], fields[2], fields[3], false,cur_apt);
                            }
                            break;

                        case "RV":
                            Condoor.AddAVisitor(fields[2], Convert.ToInt32(fields[1]), fields[3], cur_apt);
                            break;
                    }
                }
                Console.WriteLine("\nxxxxxxxx Transfer Succesfull xxxxxxxxxx");
                Condoor.GetReport();

                bool stop = true;
                while (stop)
                {
                    Console.WriteLine("\n### Menu ###");
                    Console.WriteLine("\n1. Add new apartment");
                    Console.WriteLine("2. Add a member");
                    Console.WriteLine("3. Add a visiter");
                    Console.WriteLine("4. Remove a Visitor");
                    Console.WriteLine("5. Add a vehicle");
                    Console.WriteLine("6. Update Vehicle Status");
                    Console.WriteLine("7. Get report");
                    Console.WriteLine("8. Update state to file");
                    Console.WriteLine("9. Exit");

                    Console.WriteLine("\nEnter your Input :");
                    string input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            Console.WriteLine("\nEnter Apartment Number :");
                            string apt_no = Console.ReadLine();

                            Console.WriteLine("\nEnter Owner Name :");
                            string owner_name = Console.ReadLine();

                            Console.WriteLine("\nEnter Type of Apartment :");
                            string type = Console.ReadLine();

                            Console.WriteLine("\nIs it occupied ('y' if yes anything else) :");
                            string isO = Console.ReadLine();

                            Condoor.AddAppartment(apt_no, owner_name, type, isO.Equals('y') ? true : false);
                            break;

                        case "2":

                            Console.WriteLine("\nEnter Apartment Number :");
                            string apt_num = Console.ReadLine();

                            Console.WriteLine("\nEnter member Name :");
                            string name = Console.ReadLine();

                            Console.WriteLine("\nEnter Occupation (space and enter to skip) :");
                            string occ = Console.ReadLine();

                            Console.WriteLine("\nEnter relationship with owner :");
                            string rel = Console.ReadLine();

                            Console.WriteLine("\nEnter DOB:");
                            string dob = Console.ReadLine();

                            Condoor.AddFamilyMember(apt_num, name, dob, rel, occ.Equals(' ') ? "none" : occ);
                            break;

                        case "3":

                            Console.WriteLine("\nEnter Apartment Number :");
                            string apt = Console.ReadLine();

                            Console.WriteLine("\nEnter visitor Name :");
                            string n = Console.ReadLine();

                            Console.WriteLine("\nEnter ID :");
                            string id = Console.ReadLine();

                            Console.WriteLine("\nEnter relationship with owner :");
                            string res = Console.ReadLine();

                            Condoor.AddAVisitor(n, Convert.ToInt32(id), res, apt);
                            break;

                        case "4":

                            Console.WriteLine("\nEnter Apartment Number :");
                            string ap_num = Console.ReadLine();

                            Console.WriteLine("\nEnter visitor name :");
                            string Vn = Console.ReadLine();

                            Condoor.RemoveVisitor(Vn, ap_num);
                            break;

                        case "5":

                            Console.WriteLine("\nEnter Plate Number :");
                            string lp = Console.ReadLine();

                            Console.WriteLine("\nEnter Vehicle type:");
                            string typ = Console.ReadLine();

                            Console.WriteLine("\nEnter vehicle model :");
                            string model = Console.ReadLine();

                            Console.WriteLine("\nEnter apartment number :");
                            string ano = Console.ReadLine();

                            Condoor.AddAVehicle(lp, typ, model, true,ano);
                            break;

                        case "6":

                            Console.WriteLine("\nVehicle Number :");
                            string v = Console.ReadLine();

                            Console.WriteLine("\nEnter Apartment number:");
                            string a = Console.ReadLine();

                            Console.WriteLine("\nEnter status 'in' or 'out' :");
                            string st = Console.ReadLine();


                            Condoor.UpdateVehicleStatus(v, a, st);
                            break;

                        case "7":

                            Condoor.GetReport();

                            break;
                        case "8":
                            //Condoor.UpdateState();
                            break;
                        case "9":
                            stop = false;
                            break;
                        default:
                            Console.WriteLine("Invalid Entry");
                            break;
                    }

                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error - {e.Message}");
            }
            

        }
    }
}
