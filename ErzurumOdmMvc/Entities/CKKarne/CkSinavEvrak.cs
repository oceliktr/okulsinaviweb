using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ErzurumOdmMvc.Entities.CKKarne
{
    [Dapper.Contrib.Extensions.Table("cksinavevrak")]
    public class CkSinavEvrak
    {
        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }
        [DisplayName("Sınav Adı"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string SinavAdi { get; set; }
        [DisplayName("Url"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(150, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Url { get; set; }
        [DisplayName("Kurumlar"), Required(ErrorMessage = "{0} alanı gereklidir.")]
        public string Kurumlar { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public int Hit { get; set; }
    }
}