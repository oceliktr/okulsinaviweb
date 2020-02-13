using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ErzurumOdmMvc.Entities
{
    [Dapper.Contrib.Extensions.Table("ayarlar")]
    public class SiteAyar
    {
        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }
        [DisplayName("Site Ad�"), Required(ErrorMessage = "{0} alan� gereklidir."), StringLength(150, ErrorMessage = "{0} alan� max. {1} karakter olmal�d�r.")]
        public string SiteAdi { get; set; }
        [Required(ErrorMessage = "{0} alan� gereklidir."), StringLength(250, ErrorMessage = "{0} alan� max. {1} karakter olmal�d�r.")]
        public string SiteAdres { get; set; }
        [StringLength(80)]
        public string SiteMail { get; set; }
        [StringLength(80)]
        public string MailServer { get; set; }
        public int MailPort { get; set; }
        [StringLength(80)]
        public string MailAdresi { get; set; }
        [StringLength(20)]
        public string MailSifresi { get; set; }
        [StringLength(80)]
        public string MailGonderenIsim { get; set; }

        [StringLength(80)]
        public string AliciMailAdresi { get; set; }
        public int SinavId { get; set; }
        public int VeriGirisi { get; set; }
    }
}
