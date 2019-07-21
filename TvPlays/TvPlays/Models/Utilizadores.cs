using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TvPlays.Models
{
    public class Utilizadores
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nome Completo")]
        //[RegularExpression("[A-ZÁÂÉÍÓÚ][a-záàâãäèéêëìíîïòóôõöùúûüç]+((-| )((da|de|do|das|dos) )?[A-ZÁÂÉÍÓÚ][a-záàâãäèéêëìíîïòóôõöùúûüç]+)+", ErrorMessage = "Insira o seu Nome Completo")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data Nascimento")]
        [Required(ErrorMessage = "O {0} e de preenchimento obrigatorio ")]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(13)]
        [RegularExpression("[0-9]{9}", ErrorMessage = "Insere 9 algarismos")]
        [Display(Name = "Telefone")]
        public string MobileNumber { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        //[Required]
        [StringLength(30)]
        [Index(IsUnique = true)]
        [Display(Name = "Email")]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Insira um e-mail válido")]
        public string Email { get; set; }

        [Required]
        [StringLength(1)]
        [RegularExpression("[MmFf]", ErrorMessage = "Insira F/f para Feminino, M/m para Masculino")]
        [Display(Name = "Sexo")]
        public string Sexo { get; set; }

        //ICollection de Clips
        public virtual ICollection<Clips> ListClips { get; set; }

        //ICollection de Payments
        public virtual ICollection<Payments> ListPayments { get; set; }
    }
}