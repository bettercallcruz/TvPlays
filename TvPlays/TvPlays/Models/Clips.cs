using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TvPlays.Models
{
    public class Clips
    {
        public int ID { get; set; }
        public TimeSpan TimeClip { get; set; }
        public string TitleClip { get; set; }
        public DateTime DateClip { get; set; }
        public string PathClip { get; set; }

        //Foreign Key para a tabela Utilizadores
        [ForeignKey("Utilizadores")]
        public int UtilizadoresFK { get; set; }
        public Utilizadores Utilizadores { get; set; }

        //ICollection de Comentatiros
        public ICollection<Comments> ListComments { get; set; }


    }
}