using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODM.CKYazdirDb.Entities
{
    [Table("Sablonlar")]
   public class CkSablon
    {
        [Key]
        public int Id { get; set; }
        public int Sinif { get; set; }
        public string Sablon { get; set; }
    }
}
