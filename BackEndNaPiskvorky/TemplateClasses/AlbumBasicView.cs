using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndNaPiskvorky.TemplateClasses
{
    public class AlbumBasicView
    {
        public int AlbumId { get; set; }
        public string AlbumName { get; set; } // string; 
        public string AlbumGenre { get; set; } // string; 
        public string AlbumImage { get; set; }
        public string ArtistName { get; set; } // String;
        public int NumberOfSongs { get; set; }
    }
}
