using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ErzurumOdmMvc.Entities.CKKarne
{
    [Dapper.Contrib.Extensions.Table("cksinavadi")]
    public class CkSinavAdi
    {
        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }
        public int SinavId { get; set; }
        public int Aktif { get; set; }
        public int DegerlendirmeTuru { get; set; }
        [DisplayName("Sınav Adı"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string SinavAdi { get; set; }
    }
}