using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ErzurumOdmMvc.Entities.CKKarne
{
    [Dapper.Contrib.Extensions.Table("ckkarnekazanimlar")]
    public class CkKarneKazanimlar
    {
        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }
        public int KazanimId { get; set; }
        public int SinavId { get; set; }
        public int Sinif { get; set; }
        public int BransId { get; set; }
        [DisplayName("Kazanım No"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(55, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string KazanimNo { get; set; }
        [DisplayName("Kazanım"), Required(ErrorMessage = "{0} alanı gereklidir.")]
        public string KazanimAdi { get; set; }
        [DisplayName("Kazanım (Öğrenci)"), Required(ErrorMessage = "{0} alanı gereklidir.")]
        public string KazanimAdiOgrenci { get; set; }
        [DisplayName("Soruları"), Required(ErrorMessage = "{0} alanı gereklidir.")]
        public string Sorulari { get; set; }
    }
}