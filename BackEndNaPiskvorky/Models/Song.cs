using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndNaPiskvorky.Models
{
    public class Song
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public int Length { get; set; }

        [Required]
        public int AlbumId { get; set; }

        [Required]
        public Album Album { get; set; }

    }
}
