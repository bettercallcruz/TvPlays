using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TvPlays.Models
{
    public class Utilizadores
    {

        public int ID { get; set; }
        [Required(ErrorMessage = "O {0} e de preenchimento obrigatorio ")]
        public string Name { get; set; }
        [Required(ErrorMessage = "O {1} e de preenchimento obrigatorio ")]
        public DateTime BirthDate { get; set; }
        public string MobileNumber { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }

        //ICollection de Emojis
        public ICollection<Emojis> ListEmojis { get; set; }

        //ICollection de Clips
        public ICollection<Clips> ListClips { get; set; }
    }
}