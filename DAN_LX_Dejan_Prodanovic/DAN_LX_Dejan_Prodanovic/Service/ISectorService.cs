using DAN_LX_Dejan_Prodanovic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_LX_Dejan_Prodanovic.Service
{
    interface ISectorService
    {
        tblSector GetSectorByID(int id);
        tblSector GetSectorByName(string sector);
        tblSector AddSector(tblSector sector);
    }
}
