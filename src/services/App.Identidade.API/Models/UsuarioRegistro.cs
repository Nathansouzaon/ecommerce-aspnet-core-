﻿using System.ComponentModel.DataAnnotations;

namespace App.Identidade.API.Models
{
    public class UsuarioRegistro
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 8)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Compare("Senha", ErrorMessage = "As senhas informadas são diferentes")]
        public string SenhaConfirmacao { get; set; }
    }
}