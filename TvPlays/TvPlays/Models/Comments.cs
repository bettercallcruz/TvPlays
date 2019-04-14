using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TvPlays.Models
{
    public class Comments
    {
        public int idComment { get; set; }
        public string contComment { get; set; }
        public string nameComment { get; set; }
        public DateTime dateComment { get; set; }


        [ForeignKey("Clips")]
        public int ClipsFK { get; set; }
        public Clips Clip { get; set; }
    }
}