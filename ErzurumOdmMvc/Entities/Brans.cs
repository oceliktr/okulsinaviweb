using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ErzurumOdmMvc.Entities
{
    [Dapper.Contrib.Extensions.Table("branslar")]
    public class Brans
    {
        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }
        [DisplayName("Branş Adı"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string BransAdi { get; set; }

    }
}