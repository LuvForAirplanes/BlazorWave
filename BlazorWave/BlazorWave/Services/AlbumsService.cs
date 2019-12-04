using BlazorWave.Data;
using BlazorWave.Data.Models;
using BlazorWave.Data.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BlazorWave.Services
{
    public class AlbumsService
    {
        public ApplicationDbContext context;
        private IConfiguration configuration;

        public AlbumsService(ApplicationDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public async Task<Album> GetAlbumAsync(string id)
        {
            return await context.Albums.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Album>> ListAsync()
        {
            return await context.Albums.ToListAsync();
        }

        public async Task<Album> CreateAsync(AlbumUploadDTO uploadAlbum, string ownerId = "")
        {
            //manage the database stuff
            var album = new Album
            {
                Id = uploadAlbum.Id,
                Added = DateTime.Now,
                Artist = uploadAlbum.Artist,
                Genre = uploadAlbum.Genre,
                //OwnerId = ownerId, 
                Released = uploadAlbum.Released,
                Title = uploadAlbum.Title
            };

            //manage the file stuff
            var filepath = configuration.GetSection("Main")["FilePath"];

            var tempFileName = filepath + @"\" + album.Title + @"\";

            if (!Directory.Exists(tempFileName))
                Directory.CreateDirectory(tempFileName);

            foreach (var track in uploadAlbum.Tracks)
            {
                var path = tempFileName + track.Name + "." + track.Extension;

                using (var writer = System.IO.File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
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

            using (var writer = System.IO.File.Open(artPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
            {
                writer.Write(System.Text.Encoding.ASCII.GetBytes(uploadAlbum.AlbumArtAsPng));
                //writer.Write(Convert.FromBase64String(uploadAlbum.AlbumArtAsPng));
            }

            album.AlbumArtPath = artPath;

            context.Add(album);
            await context.SaveChangesAsync();
            return await context.Albums.FirstOrDefaultAsync(a => a.Id == album.Id);
        }

        public async Task AddTrackAsync(TrackUploadDTO trackUpload, string albumId) 
        {
            var album = await context.Albums.FirstOrDefaultAsync(a => a.Id == albumId);

            var filepath = configuration.GetSection("Main")["FilePath"];

            var tempFileName = filepath + @"\" + album.Title + @"\";

            if (!Directory.Exists(tempFileName))
                Directory.CreateDirectory(tempFileName);

            var path = tempFileName + trackUpload.Name + "." + trackUpload.Extension;

            using (var writer = System.IO.File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
            {
                writer.Write(trackUpload.Data);
            }

            album.Tracks.Add(new Track
            {
                Id = Guid.NewGuid().ToString(),
                Added = DateTime.Now,
                Location = path,
                Order = trackUpload.Order,
                Title = trackUpload.Name
            });

            await context.SaveChangesAsync();
        }
    }
}
