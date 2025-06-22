using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicLife.Api.Attributes;
using MusicLife.Application.Modules.M_Album.DTOs;
using MusicLife.Application.Modules.M_Album.Services;

namespace MusicLife.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;
        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }
        [HttpGet]
        [Cache(100)]
        public async Task<IActionResult> GetAllAlbumWithPaged([FromQuery] int? page, [FromQuery] int? pageSize)
        {
            var alnums = await _albumService.GetAlbumsAsync(page, pageSize);
            return Ok(alnums);
        }
        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreatAlbum([FromForm] CreateAlbumDTO albumDTO)
        {
            var album = await _albumService.CreatAlbumAsync(albumDTO);
            return Ok(album);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlbumById(Guid id)
        {
            var album = await _albumService.GetAlbumByIdAsync(id);
            return Ok(album);
        }
        [HttpGet("songs/{id}")]
        public async Task<IActionResult> GetAllSong(Guid id)
        {
            var songs = await _albumService.GetAllSongOfAlbumAsync(id);
            return Ok(songs);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAlbum(Guid id)
        {
            await _albumService.DeleteAlbumAsync(id);
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAlbum( Guid id, [FromBody] UpdateAlbumDTO albumDTO)
        {
            var album = await _albumService.UpdateAlbumAsync(id, albumDTO);
            return Ok(album);
        }      

        [HttpPut("{albumId}/{songId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddSongToAlbum(Guid albumId, [FromRoute] Guid songId)
        {
            await _albumService.AddSongToAlbumAsync(albumId, songId);
            return Ok();
        }
    }
}
