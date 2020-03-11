using System.Collections.Generic;

namespace ErzurumOdmMvc.CKKarneModel
{
    /// <summary>
    /// Summary description for KarneModel
    /// </summary>
    public class KarneModel
    {

        public List<TableModel> TableModeller { get; set; }
        public List<string> EksikKazanimlar { get; set; }
        public string Raporu { get; set; }

    }
}