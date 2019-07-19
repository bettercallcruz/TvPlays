using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TvPlays.Models
{
    public class Clips
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Título do  Clip")]
        public string TitleClip { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Upload")]
        public DateTime DateClip { get; set; }

        [Required]
        [Display(Name = "Diretoria do Clip")]
        public string PathClip { get; set; }

        //Foreign Key para a tabela Utilizadores
        [ForeignKey("User")]
        public int UserFK { get; set; }
        public virtual Utilizadores User { get; set; }

        //ICollection de Comentatiros
        public virtual ICollection<Comments> ListComments { get; set; }


    }


    public class ClipsDTO {

        [Required]
        [StringLength(50)]
        [Display(Name = "Título do  Clip")]
        public string TitleClip { get; set; }

    }
}