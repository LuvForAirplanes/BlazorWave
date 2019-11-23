using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlazorWave.Data;
using BlazorWave.Data.Models;
using BlazorWave.Data.Models.DTOs;
using BlazorWave.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorWave.Controllers
{
    public class UploadController : ControllerBase
    {
        public IConfiguration config;
        public AlbumsService albumsService;

        public UploadController(IConfiguration config, AlbumsService albumsService)
        {
            this.config = config;
            this.albumsService = albumsService;
        }

        [HttpPost("/api/upload")]
        public async Task<IActionResult> Save([FromBody]AlbumUploadDTO uploadAlbum)
        {
            //manage the database stuff
            var album = new Album
            {
                Id = Guid.NewGuid().ToString(),
                Added = DateTime.Now,
                Artist = uploadAlbum.Artist,
                Genre = uploadAlbum.Genre,
                //Owner = 
                Released = uploadAlbum.Released,
                Title = uploadAlbum.Title
            };

            //manage the file stuff
            var filepath = config.GetSection("Main")["FilePath"];

            var tempFileName = filepath + @"\" + album.Title + @"\";

            if (!Directory.Exists(tempFileName))
                Directory.CreateDirectory(tempFileName);

            foreach (var track in uploadAlbum.Tracks)
            {
                var path = tempFileName + track.Name + "." + track.Extension;

                using (var writer = System.IO.File.Open(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                {
                    writer.Write(track.Data);
                }

                album.Tracks.Add(new Track
                {
                    Id = Guid.NewGuid().ToString(),
                    Added = DateTime.Now,
                    Location = path,
                    Order = track.Order,
                    Title = track.Name
                });
            }

            var artPath = tempFileName + "albumart.png";

            using (var writer = System.IO.File.Open(artPath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
            {
                writer.Write(System.Text.Encoding.ASCII.GetBytes(uploadAlbum.AlbumArtAsPng));
            }

            album.AlbumArtPath = artPath;

            await albumsService.CreateAsync(album);

            return Ok(Path.GetFileNameWithoutExtension(tempFileName));
        }
    }
}
