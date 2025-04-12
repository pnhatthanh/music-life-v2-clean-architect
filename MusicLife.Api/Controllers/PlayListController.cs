using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicLife.Application.Modules.M_PlayList.DTOs;
using MusicLife.Application.Modules.M_PlayList.Services;
using System.Security.Claims;

namespace MusicLife.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "User")]
    [ApiController]
    public class PlayListController : ControllerBase
    {
        private readonly IPlayListService _playListService;
        public PlayListController(IPlayListService playListService)
        {
            _playListService = playListService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePlayList([FromBody] CreatePlayListDTO playListDTO)
        {
            var playList = await _playListService.CreatePlayListAsync(playListDTO);
            return Ok(playList);
        }

        [HttpPut("song/{idPlayList}/{idSong}")]
        public async Task<IActionResult> AddSongToPlayList(Guid idPlayList, Guid idSong)
        {
            await _playListService.AddSongAsync(idPlayList, idSong);
            return Ok();
        }
        [HttpGet("user")]
        public async Task<IActionResult> GetAllPlayList()
        {
            var playLists = await _playListService.GetPlayListsAsync();
            return Ok(playLists);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayListById(Guid id)
        {
            var playList = await _playListService.GetPlayListByIdAsync(id);
            return Ok(playList);
        }

        [HttpGet("{id}/songs")]
        public async Task<IActionResult> GetSongs(Guid id)
        {
            var songs = await _playListService.GetSongsAsync(id);
            return Ok(songs);
        }

        [HttpPut("remove/song/{idPlayList}/{idSong}")]
        public async Task<IActionResult> RemoveSongFromPlayList(Guid idPlayList, Guid idSong)
        {
            await _playListService.RemoveSongAsync(idPlayList, idSong);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePlayList([FromRoute] Guid id)
        {
            await _playListService.DeletePlayListAsync(id);
            return Ok();
        }
    }
}
