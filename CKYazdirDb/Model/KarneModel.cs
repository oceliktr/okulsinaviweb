using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODM.CKYazdirDb.Library;

namespace ODM.CKYazdirDb.Model
{
    /// <summary>
    ///Karne Modeli karnenin içeriğini saklar.
    /// </summary>
    public class KarneModel
    {
        public List<TableModel> TableModeller { get; set; }
        public List<string> EksikKazanimlar { get; set; }
        public string Raporu { get; set; }
    }
}
