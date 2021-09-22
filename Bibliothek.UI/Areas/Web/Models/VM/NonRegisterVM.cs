using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bibliothek.UI.Areas.Web.Models.VM
{
    public class NonRegisterVM
    {
        [DisplayName("Adı"),
            Required(ErrorMessage = "{0} boş geçilemez!"),
            StringLength(30, ErrorMessage = "En fazla {1} karakter olmalı!")]

        public string Name { get; set; }

        [DisplayName("Soyadı"),
            Required(ErrorMessage = "{0} boş geçilemez!"),
            StringLength(30, ErrorMessage = "En fazla {1} karakter olmalı!")]

        public string LastName { get; set; }


        [DisplayName("E-Posta"),
            Required(ErrorMessage = "{0} boş geçilemez!"),
            EmailAddress(ErrorMessage = "{0} için geçerli bir e-posta adresi giriniz!"),
            StringLength(50, ErrorMessage = "En fazla {1} karakter olmalı!")]
        public string Email { get; set; }

        [DisplayName("Telefon"),
            Required(ErrorMessage = "{0} boş geçilemez!")]
        public string Phone { get; set; }



    }
}