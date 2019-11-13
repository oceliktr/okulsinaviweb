using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODM.CKYazdirDb.Model
{
    [Table("OgrenciCevaplari")]
    public class OgrenciCevabi
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public long OpaqId { get; set; }
        public string Ilce { get; set; }
        public int KurumKodu { get; set; }
        public int Sinif { get; set; }
        public string Sube { get; set; }
        [StringLength(3)]
        public string KatilimDurumu { get; set; }
        [StringLength(3)]
        public string KitapcikTuru { get; set; }
        [StringLength(350)]
        public string Cevaplar { get; set; }
    }
}
