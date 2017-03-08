using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogTeste1.Models
{
    public class RegistroViewModel
    {
        [Required]
        public string LoginName { get; set; }

        [Required] [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required][DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required][Compare("Senha")]
        public string ConfirmacaoSenha { get; set; }

    }
}