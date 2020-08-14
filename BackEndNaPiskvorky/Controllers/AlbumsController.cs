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
    public class AlbumsController : ControllerBase
    {
        private readonly PiskvorkyContext _context;

        public AlbumsController(PiskvorkyContext context)
        {
            _context = context;
        }

        // GET: api/Albums
        [HttpGet]
        public IEnumerable<Album> GetAlbum()
        {
            return _context.Album;
        }

        [HttpGet("View")]
        public IEnumerable<AlbumBasicView> GetAlbumBasicView()
        {
            return _context.Album.
                                    Join(_context.Artist,
                                        album => album.ArtistId,
                                        artist => artist.Id,
                                        (album, artist) => new AlbumBasicView
                                        {
                                            AlbumId = album.Id,
                                            AlbumName = album.Name,
                                            AlbumGenre = album.Genre,
                                            AlbumImage = album.LinkToImage,
                                            ArtistName = artist.Name,
                                            NumberOfSongs = album.Songs.Count,
                                            }
                                       ).ToList(); 
            //var x = _context.Album.Select(album =>  )
        }

        // GET: api/Albums/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlbum([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var album = await _context.Album.FindAsync(id);

            if (album == null)
            {
                return NotFound();
            }

            return Ok(album);
        }


        // GET: api/Albums/5
        [HttpGet("EditView/{id}")]
        public async Task<IActionResult> GetAlbumToEditView([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var album = await _context.Album.FindAsync(id);
            var artistName =  _context.Artist
                                            .Where(art => art.Id == album.ArtistId)
                                            .ToList()
                                            .Select(art => art.Name)
                                            .First(); ;

            if (album == null)
            {
                return NotFound();
            }

            AlbumBasicUpdate albumUpdate = new AlbumBasicUpdate()
            {
                Id = album.Id,
                Name = album.Name,
                Genre = album.Genre,
                YearOfRelease = album.YearOfRelease,
                AlbumImage = album.LinkToImage,
                ArtistName = artistName,
            };

            return Ok(albumUpdate);
        }

        // PUT: api/Albums/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlbum([FromRoute] int id, [FromBody] AlbumBasicUpdate albumBasicUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            Artist artist = _context.Artist.Where(art => art.Name == albumBasicUpdate.ArtistName).ToList().First();
            int artistId = _context.Artist
                                            .Where(art => art.Name == albumBasicUpdate.ArtistName)
                                            .ToList()
                                            .Select(art => art.Id)
                                            .First();

            Album newAlbum = new Album(albumBasicUpdate.Genre,
                                       albumBasicUpdate.YearOfRelease,
                                       albumBasicUpdate.Name,
                                       albumBasicUpdate.AlbumImage,
                                       artistId,
                                       artist);

            newAlbum.Id = albumBasicUpdate.Id;



            if (id != newAlbum.Id)
            {
                return BadRequest();
            }

            _context.Entry(newAlbum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbumExists(id))
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

        // POST: api/Albums
        [HttpPost]
        public async Task<IActionResult> PostAlbum([FromBody] Album album)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Album.Add(album);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlbum", new { id = album.Id }, album);
        }

        [HttpPost("Input")]
        public async Task<IActionResult> PostAlbumBasicInput([FromBody] AlbumBasicInput albumBasicInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Artist artist = _context.Artist.Where(art => art.Name == albumBasicInput.ArtistName).ToList().First();
            int artistId  = _context.Artist
                                            .Where(art => art.Name == albumBasicInput.ArtistName)
                                            .ToList()
                                            .Select(art => art.Id)
                                            .First();

            Album newAlbum = new Album(albumBasicInput.Genre, 
                                       albumBasicInput.YearOfRelease,
                                       albumBasicInput.Name,
                                       albumBasicInput.AlbumImage,
                                       artistId,
                                       artist);

            _context.Album.Add(newAlbum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlbum", new { id = newAlbum.Id }, newAlbum);
        }


        // DELETE: api/Albums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var album = await _context.Album.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            _context.Album.Remove(album);
            await _context.SaveChangesAsync();

            return Ok(album);
        }

        private bool AlbumExists(int id)
        {
            return _context.Album.Any(e => e.Id == id);
        }
    }

  
}