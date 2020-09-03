using DAN_LX_Dejan_Prodanovic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_LX_Dejan_Prodanovic.Service
{
    class SectorService:ISectorService
    {
        public tblSector AddSector(tblSector sector)
        {
            try
            {
                using (EmployeeDbEntities1 context = new EmployeeDbEntities1())
                {
                    tblSector newSector = new tblSector();

                    newSector.SectorName = sector.SectorName;

                    context.tblSectors.Add(newSector);

                    context.SaveChanges();

                    

                    sector.SectorID = newSector.SectorID;
                    return sector;

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public tblSector GetSectorByID(int id)
        {
            try
            {
                using (EmployeeDbEntities1 context = new EmployeeDbEntities1())
                {
                    tblSector sectorFromDB = (from s in context.tblSectors
                                              where s.SectorID==id select s).First();


                    return sectorFromDB;


                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public tblSector GetSectorByName(string sector)
        {
            try
            {
                using (EmployeeDbEntities1 context = new EmployeeDbEntities1())
                {
                    tblSector sectorFromDB = (from s in context.tblSectors where s.SectorName.Equals(sector) select s).First();


                    return sectorFromDB;

                    
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
