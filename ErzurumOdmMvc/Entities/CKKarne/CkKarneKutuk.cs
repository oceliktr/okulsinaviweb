using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ErzurumOdmMvc.Entities.CKKarne
{
    [Dapper.Contrib.Extensions.Table("ckkarnekutuk")]
    public class CkKarneKutuk
    {
        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }
        public int OpaqId { get; set; }
        [DisplayName("İlçe Adı"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string IlceAdi { get; set; }
        public int KurumKodu { get; set; }
        [DisplayName("Kurum Adı"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(80, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string KurumAdi { get; set; }
        public int OgrenciNo { get; set; }
        [DisplayName("Öğrenci Adı"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Adi { get; set; }
        [DisplayName("Öğrenci Soyadı"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Soyadi { get; set; }
        public int Sinifi { get; set; }
        [DisplayName("Şube"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(3, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Sube { get; set; }
        public int SinavId { get; set; }
        public int DersKodu { get; set; }
        [DisplayName("Katılım Durumu"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(3, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string KatilimDurumu { get; set; }
        [DisplayName("Kitapçık Türü"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(3, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string KitapcikTuru { get; set; }
        [DisplayName("Öğrenci Cevapları"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(350, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Cevaplar { get; set; }
    }
}