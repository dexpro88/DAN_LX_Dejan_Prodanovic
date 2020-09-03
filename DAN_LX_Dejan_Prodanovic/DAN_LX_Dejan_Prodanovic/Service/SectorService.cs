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
                using (EmployeeDbEntities context = new EmployeeDbEntities())
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

        public tblSector GetSectorByName(string sector)
        {
            try
            {
                using (EmployeeDbEntities context = new EmployeeDbEntities())
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
