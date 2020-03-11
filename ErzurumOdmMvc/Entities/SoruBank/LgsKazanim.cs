using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ErzurumOdmMvc.Entities.SoruBank
{
    [Dapper.Contrib.Extensions.Table("lgskazanimlar")]
    public class LgsKazanim
    {
        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }
        public int BransId { get; set; }
        public int Sinif { get; set; }
        [DisplayName("Kazanım No"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string KazanimNo { get; set; }
        [DisplayName("Kazanım Adı"), Required(ErrorMessage = "{0} alanı gereklidir.")]
        public string Kazanim { get; set; }
    }
}