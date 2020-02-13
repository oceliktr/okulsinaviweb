using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ErzurumOdmMvc.Entities
{
    [Dapper.Contrib.Extensions.Table("ilceler")]
    public class Ilce
    {
        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }
        [DisplayName("İlçe Adı"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string IlceAdi { get; set; }

    }
}