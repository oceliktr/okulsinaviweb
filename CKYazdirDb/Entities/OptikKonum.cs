using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODM.CKYazdirDb.Entities
{
    [Table("OptikKonumlar")]
    public class OptikKonum
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int OgrBilgiX { get; set; }
        public int OgrBilgiY { get; set; }
        public int OgrBilgiH { get; set; }
        public int BubleW { get; set; }
        public int BubleH { get; set; }
        public int BubleX { get; set; }
        public int BubleY { get; set; }
        public int BubleArtim { get; set; }
        [StringLength(350)]
        public string Sablon { get; set; }
    }
}
