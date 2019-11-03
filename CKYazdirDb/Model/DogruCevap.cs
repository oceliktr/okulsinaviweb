using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODM.CKYazdirDb
{
    [Table("DogruCevaplar")]
  public  class DogruCevap
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SinavId { get; set; }
        public int BransId { get; set; }
        [StringLength(3)]
        public string KitapcikTuru { get; set; }
        [StringLength(250)]
        public string Cevaplar { get; set; }
        
    }
}
