using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TvPlays.Models
{
    public class Clips
    {
        public int idClip { get; set; }
        public TimeSpan timeClip { get; set; }
        public string titleClip { get; set; }
        public DateTime dateClip { get; set; }
        public DateTimeOffset sizeClip { get; set; }
        public string pathClip { get; set; }

        public ICollection<Comments> listComments { get; set; }


    }
}