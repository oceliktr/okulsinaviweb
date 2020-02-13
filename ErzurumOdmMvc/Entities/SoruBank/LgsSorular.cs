using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ErzurumOdmMvc.Entities.SoruBank
{
    [Dapper.Contrib.Extensions.Table("lgssorular")]
    public class LgsSorular
    {
        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public int SinavId { get; set; }
        public int BransId { get; set; }
        public int Sinif { get; set; }
        public int KazanimId { get; set; }
        public string Kazanim { get; set; }
        [DisplayName("Soru Url"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(250, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string SoruUrl { get; set; }
        public DateTime Tarih { get; set; }
        public int Onay { get; set; }
    }
}