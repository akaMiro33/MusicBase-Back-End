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
    [Produces("application/json")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly PiskvorkyContext _context;

        public ArtistsController(PiskvorkyContext context)
        {
            _context = context;
        }

        // GET: api/Artists
        [HttpGet]
        public IEnumerable<Artist> GetArtist()
        {
            return _context.Artist;
        }

        [HttpGet("View")]
        public IEnumerable<ArtistBasicView> GetArtistBasicView()
       {
           

            return _context.Artist.Join(_context.Album,
                                       artist => artist.Id,
                                       album => album.ArtistId,
                                       (artist, album) => new ArtistBasicView
                                       {

                                           ArtistId = artist.Id,
                                           ArtistGenre = artist.Genre,
                                           ArtistName = artist.Name,
                                           ArtistImage = artist.LinkToImage,
                                           NumberOfAlbums = artist.Albums.Count,
                                           //NumberOfSongs = album.Songs.Count
                                           NumberOfSongs = artist.Albums.Sum(al => al.Songs.Count)
                                       });



        }

        // GET: api/Artists/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtist([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var artist = await _context.Artist.FindAsync(id);


            if (artist == null)
            {
                return NotFound();
            }

            return Ok(artist);
        }

        [HttpGet("EditView/{id}")]
        public async Task<IActionResult> GetArtistToEditVIew([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

         
            var art = await _context.Artist.Join(_context.Album,
                                  artist => artist.Id,
                                  album => album.ArtistId,
                                  (artist, album) => new ArtistBasicView
                                  {
                                      ArtistId = artist.Id,
                                      ArtistGenre = artist.Genre,
                                      ArtistName = artist.Name,
                                      ArtistImage = artist.LinkToImage,
                                      NumberOfAlbums = artist.Albums.Count,
                                           //NumberOfSongs = album.Songs.Count
                                           NumberOfSongs = artist.Albums.Sum(al => al.Songs.Count)

                                  }).Where(x => x.ArtistId == id).FirstAsync();



            if (art == null)
            {
                return NotFound();
            }

            return Ok(art);
        }

        // PUT: api/Artists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtist([FromRoute] int id, [FromBody] Artist artist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artist.Id)
            {
                return BadRequest();
            }

            _context.Entry(artist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistExists(id))
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

        // POST: api/Artists
        [HttpPost]
        public async Task<IActionResult> PostArtist([FromBody] Artist artist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Artist.Add(artist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArtist", new { id = artist.Id }, artist);
        }

        // DELETE: api/Artists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var artist = await _context.Artist.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            _context.Artist.Remove(artist);
            await _context.SaveChangesAsync();

            return Ok(artist);
        }

        private bool ArtistExists(int id)
        {
            return _context.Artist.Any(e => e.Id == id);
        }

        private void numberOfSongOfArtist(Album album)
        {
    
            /*int numberOfSong = 0;
            artist.Albums.ToList().ForEach(album =>
                                    {
                                        if (!(album.Songs == null))
                                        {
                                            numberOfSong = numberOfSong + album.Songs.Count;
                                        }
                                    } 
            );
            return numberOfSong;*/
        }
    }
}