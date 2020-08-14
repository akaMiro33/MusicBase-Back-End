using BackEndNaPiskvorky.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndNaPiskvorky.Models
{
    public class Album
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public int YearOfRelease { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LinkToImage { get; set; }

        [Required]
        public int ArtistId { get; set; }

        [Required]
        public Artist Artist { get; set; }

        public ICollection<Song> Songs { get; set; }

        public Album(string genre, int yearOfRelease,
                     string name, string linkToImage,
                     int artistId, Artist artist)
        {
            Genre = genre;
            YearOfRelease = yearOfRelease;
            Name = name;
            LinkToImage = linkToImage;
            ArtistId = artistId;
            Artist = artist;
            Songs = new List<Song>();


        }

        public Album()
        {

        }
    }
}
