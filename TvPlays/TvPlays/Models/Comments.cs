﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TvPlays.Models
{
    public class Comments
    {
        public int ID { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Conteudo do Comentário")]
        public string ContComment { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Data do Comentário")]
        public DateTime DateComment { get; set; }

        //Foreign Key para a tabela Clips
        [ForeignKey("Clips")]
        public int ClipsFK { get; set; }
        public Clips Clips { get; set; }

        //Foreign Key para a tabela Utilizadores
        [ForeignKey("Utilizadores")]
        public int UtilizadoresFK { get; set; }
        public Utilizadores Utilizadores { get; set; }
    }
}