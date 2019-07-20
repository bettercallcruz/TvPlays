using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TvPlays.Models
{
    public class Payments
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Valor do Pagamento")]
        public ValueList Value { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Dia do Pagamento")]
        public DateTime PaymentDay { get; set; }

        [ForeignKey("User")]
        public int UtilizadoresFK { get; set; }
        public virtual Utilizadores User { get; set; }

        //10€ = 1 mes || 50€ = 6 mes || 80€ = 12 mes
        public enum ValueList
        {
            [Display(Name = "10€")]
            Dez = 1,
            [Display(Name = "50€")]
            Cinquenta = 6,
            [Display(Name = "80€")]
            Oitenta = 12
        }
    }


}