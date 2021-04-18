using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AGENDA_BR.Models
{
    public class Pessoa
    {
        public int PessoaID { get; set; }
        [Required(ErrorMessage ="O nome é um item obrigatório, favor preencher!")]
        [StringLength(30, ErrorMessage = "Nome muito extenso.")]

        public string Nome { get; set; }
        [Required(ErrorMessage = "O Sobrenome é um item obrigatório, favor preencher!")]
        [StringLength(30, ErrorMessage = "sobrenome muito extenso.")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "A idade é um item obrigatório, favor preencher!")]
        [Range(18, int.MaxValue, ErrorMessage ="Idade inapropriada, favor rever.")]
        public int Idade { get; set; }

        [Required(ErrorMessage = "O email é um item obrigatório, favor preencher!")]
        [StringLength(40, ErrorMessage = "Email muito extenso.")]
        [EmailAddress (ErrorMessage ="Email inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage ="{0} é obrigatório")]
        public tipoTelefone TipoTelefone { get; set; }

        public string Foto { get; set; }
    }
    public enum tipoTelefone
    { 
       Publico,Presencial
    }

}
