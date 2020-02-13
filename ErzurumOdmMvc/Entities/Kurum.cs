using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ErzurumOdmMvc.Entities
{
    [Dapper.Contrib.Extensions.Table("kurumlar")]
    public class Kurum
    {
        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }
        public int IlceId { get; set; }
        [DisplayName("Kurum Kodu"), Required(ErrorMessage = "{0} alanı gereklidir.")]
        public int KurumKodu { get; set; }
        [DisplayName("Kurum Adı"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(80, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string KurumAdi { get; set; }
        [DisplayName("E-posta Adresi"), StringLength(55, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Email { get; set; }
        [DisplayName("Kurum Türü"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string KurumTuru { get; set; }
        [DisplayName("Tür"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Tur { get; set; }
        public bool Kapali { get; set; }
    }
}