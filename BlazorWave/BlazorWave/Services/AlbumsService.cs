using BlazorWave.Data;
using BlazorWave.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWave.Services
{
    public class AlbumsService
    {
        private ApplicationDbContext context;

        public AlbumsService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Album>> ListAsync()
        {
            return await context.Albums.ToListAsync();
        }

        public async Task<Album> CreateAsync(Album album)
        {
            context.Add(album);
            await context.SaveChangesAsync();
            return await context.Albums.FirstOrDefaultAsync(a => a.Id == album.Id);
        }
    }
}
