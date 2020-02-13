using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ErzurumOdmMvc.Entities.CKKarne
{
    [Dapper.Contrib.Extensions.Table("ckkarnedogrucevaplar")]
    public class CkKarneDogruCevaplar
    {
        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }
        public int SinavId { get; set; }
        public int Sinif { get; set; }
        public int BransId { get; set; }
        [DisplayName("Kitapçık Türü"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(3, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string KitapcikTuru { get; set; }
        [DisplayName("Cevaplar"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(250, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Cevaplar { get; set; }
    }
}