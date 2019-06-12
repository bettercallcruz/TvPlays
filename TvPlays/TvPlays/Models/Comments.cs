using System;
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
        [Display(Name = "Conteúdo do Comentário")]
        public string ContComment { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Data do Comentário")]
        public DateTime DateComment { get; set; }

        //Foreign Key para a tabela Clips
        [ForeignKey("Clip")]
        public int ClipsFK { get; set; }
        public virtual Clips Clip { get; set; }

        //Foreign Key para a tabela Utilizadores
        [ForeignKey("User")]
        public int UtilizadoresFK { get; set; }
        public virtual Utilizadores User { get; set; }
    }
}