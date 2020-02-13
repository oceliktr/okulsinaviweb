using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ErzurumOdmMvc.Entities.CKKarne
{
    [Dapper.Contrib.Extensions.Table("ckkarnelog")]
    public class CkKarneLog
    {
        public int Id { get; set; }
        public int Sinif { get; set; }
        public int Brans { get; set; }
        public int KullaniciId { get; set; }
        public int KurumKodu { get; set; }
        public DateTime Tarih { get; set; }
        public int Say { get; set; }
        [DisplayName("Açıklama"), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Aciklama { get; set; }
    }
}