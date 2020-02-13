using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ErzurumOdmMvc.Entities
{
    [Dapper.Contrib.Extensions.Table("kullanicilar")]
    public class Kullanici
    {
        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }
        public int IlceId { get; set; }
        [DisplayName("Giriş Bilgisi"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string TcKimlik { get; set; }
        public int KurumKodu { get; set; }
        [DisplayName("Adı Soyadı"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(80,MinimumLength =6, ErrorMessage = "{0} alanı {2} - {1} karakter arasında olmalıdır.")]
        public string AdiSoyadi { get; set; }
        public int Bransi { get; set; }
        [DisplayName("E-posta Adresi"), StringLength(55, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Email { get; set; }
        [DisplayName("Cep Telefonu"), StringLength(15, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string CepTlf { get; set; }
        [DisplayName("Şifre"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(80, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Sifre { get; set; }
        [DisplayName("Yetki"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Yetki { get; set; }
        [DisplayName("Grup"), StringLength(10, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Grup { get; set; }
        public DateTime OncekiGiris { get; set; }
        public DateTime SonGiris { get; set; }
        public int GirisSayisi { get; set; }
        [StringLength(50)]
        public string GirisKodu { get; set; }
    }
}