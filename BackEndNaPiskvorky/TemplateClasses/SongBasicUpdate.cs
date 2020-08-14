using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndNaPiskvorky.TemplateClasses
{
    public class SongBasicUpdate
    {
        public int Id { get; set; }
        public string AlbumName { get; set; }
        public string SongName { get; set; } // string; 
        public string SongGenre { get; set; } // string; 
        public int SongLength { get; set; }
    }
}
