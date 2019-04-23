using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TvPlays.Models
{
    public class Emojis
    {

        public int ID { get; set; }

        [Required]
        [Display(Name = "Diretoria do Emoji")]
        public string PathEmoji { get; set; }

        //ICollection de Users
        public ICollection<Utilizadores> ListUsers { get; set; }



    }
}