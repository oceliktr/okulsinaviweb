using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODM.CKYazdirDb.Entities
{
    [Table("Kazanimlar")]
    public class Kazanim
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Sinif { get; set; }
        public int BransId { get; set; }
        public string KazanimNo { get; set; }
        public string KazanimAdi { get; set; }
        public string KazanimAdiOgrenci { get; set; }
        public string Sorulari { get; set; }
    }
}
