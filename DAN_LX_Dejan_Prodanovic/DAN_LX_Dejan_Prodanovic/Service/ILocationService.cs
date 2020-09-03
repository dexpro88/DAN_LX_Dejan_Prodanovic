using DAN_LX_Dejan_Prodanovic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_LX_Dejan_Prodanovic.Service
{
    interface ILocationService
    {
        tblLocation GetLocationByID(int id);
        tblLocation AddLocation(tblLocation location);
        List<tblLocation> GetAllLocations();
    }
}
