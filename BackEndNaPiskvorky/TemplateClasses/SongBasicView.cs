using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndNaPiskvorky.TemplateClasses
{
    public class SongBasicView
    {
        public int SongId { get; set; }
        public string SongName { get; set; } // string; 
        public string SongGenre { get; set; } // string; 
        public int SongLength { get; set; }
        public string AlbumName { get; set; } // String;
        public string ArtistName { get; set; }
    }
}
