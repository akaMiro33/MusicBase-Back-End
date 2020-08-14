using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndNaPiskvorky.Models
{
    public class Artist
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string  Name { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public string LinkToImage { get; set; }

        public ICollection<Album> Albums { get; set; }
    }
}
