using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicLife.Api.Attributes;
using MusicLife.Application.Modules.M_Artist.DTOs;
using MusicLife.Application.Modules.M_Artist.Services;

namespace MusicLife.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _artistService;
        public ArtistController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet]
        [Cache(100)]
        public async Task<IActionResult> GetArtistsWithPaged([FromQuery] int? page, [FromQuery] int? pageSize)
        {
            var artists = await _artistService.GetArtistsAsync(page, pageSize);
            return Ok(artists);
        }

        [HttpGet("{id}")]
        [Cache(100)]
        public async Task<IActionResult> GetArtistById(Guid id)
        {
            var artist = await _artistService.GetArtistByIdAsync(id);
            return Ok(artist);
        }

        [HttpGet("{id}/songs")]
        [Cache(100)]
        public async Task<IActionResult> GetAllSong(Guid id, [FromQuery] int page,[FromQuery] int pageSize)
        {
            var songs = await _artistService.GetAllSongsAsync(page, pageSize, id);
            return Ok(songs);
        }

        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddArtist([FromForm] CreateArtistDTO artistDTO)
        {
            var artist = await _artistService.CreateArtistAsync(artistDTO);
            return Ok(artist);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteArtist(Guid id)
        {
            await _artistService.DeleteArtistAsync(id);
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateArtist(Guid id, [FromBody] UpdateArtistDTO artistDTO)
        {
            var artist = await _artistService.UpdateArtistAsync(id, artistDTO);
            return Ok(artist);
        }
    }
}
