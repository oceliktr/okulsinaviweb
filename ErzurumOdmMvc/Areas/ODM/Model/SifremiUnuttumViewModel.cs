using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErzurumOdmMvc.Areas.ODM.Model
{
   public class SifremiUnuttumViewModel
    {
        [EmailAddress(ErrorMessage = "E-posta adresiniz geçerli değil"), DisplayName("E-posta Adresiniz"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Eposta { get; set; }
    }
}
