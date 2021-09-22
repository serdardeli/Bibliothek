using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bibliothek.UI.Areas.Web.Models.VM
{
    public class MailingVM
    {
        [DisplayName("E-Posta"),
            Required(ErrorMessage = "{0} boş geçilemez!"),
            EmailAddress(ErrorMessage = "{0} için geçerli bir e-posta adresi giriniz!"),
            StringLength(50, ErrorMessage = "En fazla {1} karakter olmalı!")]
        public string Email { get; set; }
    }
}