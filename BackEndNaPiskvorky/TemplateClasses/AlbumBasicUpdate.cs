using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndNaPiskvorky.TemplateClasses
{
    public class AlbumBasicUpdate
    {
        public int Id { get; set; }
        public string ArtistName { get; set; }
        public string Name { get; set; } // string; 
        public string Genre { get; set; } // string; 
        public int YearOfRelease { get; set; }
        public string AlbumImage { get; set; } // String;

    }
}
