using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndNaPiskvorky.TemplateClasses
{
    public class ArtistBasicView
    {
        public int ArtistId { get; set; }
        public string ArtistName { get; set; } // string; 
        public string ArtistGenre { get; set; } // string; 
        public string ArtistImage { get; set; }
        public int NumberOfAlbums { get; set; } // String;
        public int NumberOfSongs { get; set; }
    }
}
