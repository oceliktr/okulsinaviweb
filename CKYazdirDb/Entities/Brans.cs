using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODM.CKYazdirDb.Library;

namespace ODM.CKYazdirDb.Entities
{
    [Table("Branslar")]
    public class Brans
    {
        public Brans()
        {
        }

        public Brans(int id, string bransAdi)
        {
            BransAdi = bransAdi;
            Id = id;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string BransAdi { get; set; }


    }
}
