using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWave.Data.Models
{
    public class Album : BaseModel
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        public string AlbumArtPath { get; set; }
        public DateTime Released { get; set; }
        public List<Track> Tracks { get; set; } = new List<Track>();
        public IdentityUser Owner { get; set; }
        public string OwnerId { get; set; }
        public TimeSpan Length
        {
            get
            {
                return TimeSpan.FromSeconds(Tracks.Select(s => s.Length).Sum(l => l.TotalSeconds));
            }
        }
    }
}
