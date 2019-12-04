using BlazorWave.Data;
using BlazorWave.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KeyLogic.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly AlbumsService albumsService;
        private readonly ApplicationDbContext context;

        public MusicController(AlbumsService albumsService, ApplicationDbContext context)
        {
            this.albumsService = albumsService;
            this.context = context;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAsync(string trackId)
        {
            var track = await albumsService.context.Tracks.FirstOrDefaultAsync(t => t.Id == trackId);

            var stream = new MemoryStream();

            using (var fileWriter = System.IO.File.Open(track.Location, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
            {
                await fileWriter.CopyToAsync(stream);
            }

            stream.Position = 0;

            return new FileStreamResult(stream, "audio/mpeg") { FileDownloadName = $"music." + track.Location.Split(".").Last() };
        }
    }
}
