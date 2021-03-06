﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAN_LX_Dejan_Prodanovic.Model;

namespace DAN_LX_Dejan_Prodanovic.Service
{
    class LocationService : ILocationService
    {
        public tblLocation AddLocation(tblLocation location)
        {
            try
            {
                using (EmployeeDbEntities1 context = new EmployeeDbEntities1())
                {

                    tblLocation newLocation = new tblLocation();
                    newLocation.City = location.City;
                    newLocation.Number = location.Number;
                    newLocation.Street = location.Street;
                    newLocation.Country = location.Country;


                    context.tblLocations.Add(newLocation);
                    context.SaveChanges();
                    location.LocationID = newLocation.LocationID;

                    

                    return location;

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public List<tblLocation> GetAllLocations()
        {
            try
            {
                using (EmployeeDbEntities1 context = new EmployeeDbEntities1())
                {
                    List<tblLocation> list = new List<tblLocation>();
                    list = (from x in context.tblLocations select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public tblLocation GetLocationByID(int id)
        {
            try
            {
                using (EmployeeDbEntities1 context = new EmployeeDbEntities1())
                {
                    tblLocation locationInDb = (from s in context.tblLocations
                                              where s.LocationID == id
                                              select s).First();


                    return locationInDb;

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
    }
}
