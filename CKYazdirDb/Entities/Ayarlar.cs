using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODM.CKYazdirDb.Entities
{
    [Table("Ayarlar")]
    public class Ayarlar
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(300)]
        public string Logo { get; set; }
        [StringLength(300)]
        public string SinifListesiSablon { get; set; }
        
        [StringLength(20)]
        public string IlAdi { get; set; }
        [StringLength(100)]
        public string SinavAdi { get; set; }
        public int DegerlendirmeTuru { get; set; }
        public string OdmAdres { get; set; }      
        public string OdmWeb { get; set; }      
        public string OdmEmail { get; set; }
    }
}
