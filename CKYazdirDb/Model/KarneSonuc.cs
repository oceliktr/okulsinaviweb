using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODM.CKYazdirDb.Model
{
    [Table("KarneSonuclari")]
    public class KarneSonuc
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SinavId { get; set; }
        public int BransId { get; set; }
        public string Ilce { get; set; }
        public int KurumKodu { get; set; }
        public int Sinif { get; set; }
        public string Sube { get; set; }
        public string KitapcikTuru { get; set; }
        public int SoruNo { get; set; }
        public int Dogru { get; set; }
        public int Yanlis { get; set; }
        public int Bos { get; set; }

    }
}
