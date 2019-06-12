﻿using System;
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
        public Decimal Value { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Dia do Pagamento")]
        public DateTime PaymentDay { get; set; }

        [ForeignKey("User")]
        public int UtilizadoresFK { get; set; }
        public virtual Utilizadores User { get; set; }

    }
}