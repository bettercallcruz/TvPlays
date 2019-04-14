using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TvPlays.Models
{
    public class Comments
    {
        public int ID { get; set; }
        public string ContComment { get; set; }
        public string NameComment { get; set; }
        public DateTime DateComment { get; set; }

        //Foreign Key para a tabela Clips
        [ForeignKey("Clips")]
        public int ClipsFK { get; set; }
        public Clips Clips { get; set; }
    }
}