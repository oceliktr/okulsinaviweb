using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ErzurumOdmMvc.Entities;

namespace ErzurumOdmMvc.Areas.ODM.Model
{
    public class IlceKurumViewModel:Kurum
    {
        public string IlceAdi { get; set; }
    }
}