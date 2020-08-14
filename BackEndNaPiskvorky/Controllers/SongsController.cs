using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEndNaPiskvorky.Models;
using Piskvorky.Models;
using BackEndNaPiskvorky.TemplateClasses;

namespace BackEndNaPiskvorky.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly PiskvorkyContext _context;

        public SongsController(PiskvorkyContext context)
        {
            _context = context;
        }

        // GET: api/Songs
        [HttpGet]
        public IEnumerable<Song> GetSong()
        {
            return _context.Song;
        }

        [HttpGet("View")]
        public IEnumerable<SongBasicView> GetSongBasicView()
        {
            return _context.Song
                                    .Join(_context.Album,
                                            song => song.AlbumId,
                                            album => album.Id,
                                            (song, album) => new 
                                            {
                                                SongId = song.Id,
                                                ArtistId = album.ArtistId,
                                                SongName = song.Genre,
                                                SongGenre = song.Name,
                                                SongLength = song.Length,
                                                AlbumName = album.Name,
                                           
                                      
                                            })
                                    .Join(_context.Artist,
                                            album => album.ArtistId,
                                            artist => artist.Id,
                                            (album, artist) => new SongBasicView
                                            {
                                                SongId = album.SongId,
                                                SongName = album.SongName,
                                                SongGenre = album.SongGenre,
                                                SongLength = album.SongLength,
                                                AlbumName = album.AlbumName,
                                                ArtistName = artist.Name
                                             });

            // return _context.Song;
        }

        // GET: api/Songs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSong([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var song = await _context.Song.FindAsync(id);

            if (song == null)
            {
                return NotFound();
            }

            return Ok(song);
        }

        // PUT: api/Songs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSong([FromRoute] int id, [FromBody] Song song)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != song.Id)
            {
                return BadRequest();
            }

            _context.Entry(song).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Songs
        [HttpPost]
        public async Task<IActionResult> PostSong([FromBody] Song song)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Song.Add(song);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSong", new { id = song.Id }, song);
        }

        [HttpPost("Input")]
        public async Task<IActionResult> PostSongBasicInput([FromBody] SongBasicInput songBasicInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Album album = _context.Album.Where(alb => alb.Name == songBasicInput.AlbumName).ToList().First();
            album.Artist = _context.Artist.Where(art => art.Id == album.ArtistId).ToList().First();
            int albumId = _context.Album
                                            .Where(alb => alb.Name == songBasicInput.AlbumName)
                                            .ToList()
                                            .Select(alb => alb.Id)
                                            .First();

            Song newSong = new Song()
            {
                Name = songBasicInput.SongName,
                Genre = songBasicInput.SongGenre,
                Length = songBasicInput.SongLength,
                Album = album,
                AlbumId = albumId
            };



            _context.Song.Add(newSong);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSong", new { id = newSong.Id }, newSong);
        }

        // DELETE: api/Songs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var song = await _context.Song.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            _context.Song.Remove(song);
            await _context.SaveChangesAsync();

            return Ok(song);
        }

        private bool SongExists(int id)
        {
            return _context.Song.Any(e => e.Id == id);
        }
    }
}