using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TvPlays.Models
{
    public class Categories
    {

        public int ID { get; set; }

        [Required]
        [Display(Name = "Nome da Categoria")]
        public string Name { get; set; }

        public string PathToCategory { get; set; }

        //ICollection de Clips
        public virtual ICollection<Clips> ListClips { get; set; }

    }
}