using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODM.CKYazdirDb.Entities
{
    [Table("CevapTxt")]
    public class CevapTxt
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public long OpaqId { get; set; }
        public int BransId { get; set; }
        [StringLength(3)]
        public string KitapcikTuru { get; set; }
        public int KatilimDurumu { get; set; }
        [StringLength(250)]
        public string Cevaplar { get; set; }

        public CevapTxt(long opaqId, int bransId, string kitapcikTuru, int katilimDurumu, string cevaplar)
        {
            OpaqId = opaqId;
            BransId = bransId;
            KitapcikTuru = kitapcikTuru;
            KatilimDurumu = katilimDurumu;
            Cevaplar = cevaplar;
        }

        public CevapTxt()
        {
        }
    }
}
