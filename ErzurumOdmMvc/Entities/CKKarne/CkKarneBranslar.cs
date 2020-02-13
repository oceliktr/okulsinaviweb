using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ErzurumOdmMvc.Entities.CKKarne
{
    [Dapper.Contrib.Extensions.Table("ckkarnebranslar")]
    public class CkKarneBranslar
    {
        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }
        public int SinavId { get; set; }
        public int Sira { get; set; }
        public int BransId { get; set; }
        [DisplayName("Branş Adı"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string BransAdi { get; set; }
    }
}