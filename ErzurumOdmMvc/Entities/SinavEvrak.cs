using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ErzurumOdmMvc.Entities
{
    [Dapper.Contrib.Extensions.Table("sinavevrak")]
    public class SinavEvrak
    {
        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }
        [DisplayName("Açıklama"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(150, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Aciklama { get; set; }
        [DisplayName("Url"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(150, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Url { get; set; }
        [DisplayName("Kurumlar"), Required(ErrorMessage = "{0} alanı gereklidir.")]
        public string Kurumlar { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public int Hit { get; set; }
    }
}