using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ErzurumOdmMvc.Entities.CKKarne
{
    [Dapper.Contrib.Extensions.Table("ckkarnesonuclari")]
    public class CkKarneSonuclari
    {
        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }
        public int SinavId { get; set; }
        public int BransId { get; set; }
        [DisplayName("İlçe Adı"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Ilce { get; set; }
        public int KurumKodu { get; set; }
        public int Sinif { get; set; }
        [DisplayName("Şube"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(2, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Sube { get; set; }
        [DisplayName("Kitapçık Türü"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(2, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string KitapcikTuru { get; set; }
        public int SoruNo { get; set; }
        public int Dogru { get; set; }
        public int Yanlis { get; set; }
        public int Bos { get; set; }
        public string SinavAdi { get; set; }
        public string BransAdi { get; set; }
    }
}