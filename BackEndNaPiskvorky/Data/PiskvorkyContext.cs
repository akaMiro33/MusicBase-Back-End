using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BackEndNaPiskvorky.Models;
using BackEndNaPiskvorky.Controllers;

namespace Piskvorky.Models
{
    public class PiskvorkyContext : DbContext
    {
        public PiskvorkyContext(DbContextOptions<PiskvorkyContext> options)
            : base(options)
        {
        }

        public DbSet<BackEndNaPiskvorky.Models.User> User { get; set; }

        public DbSet<BackEndNaPiskvorky.Models.Artist> Artist { get; set; }

        public DbSet<BackEndNaPiskvorky.Models.Album> Album { get; set; }

        public DbSet<BackEndNaPiskvorky.Models.Song> Song { get; set; }

    }
}
