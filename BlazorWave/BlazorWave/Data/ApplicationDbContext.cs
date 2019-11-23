using System;
using System.Collections.Generic;
using System.Text;
using BlazorWave.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorWave.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Track> Tracks { get; set; }
        //I don't see an immediate need for this.
        //public DbSet<TrackLocation> TrackLocations { get; set; }
        public DbSet<TrackPlaylistMap> TrackPlaylistMaps { get; set; }
        public DbSet<TrackShare> TrackShares { get; set; }
        public DbSet<UserSpeedPreference> UserSpeedPreferences { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
