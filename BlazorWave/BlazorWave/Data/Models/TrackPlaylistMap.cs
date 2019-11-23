using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWave.Data.Models
{
    public class TrackPlaylistMap : BaseModel
    {
        public Playlist Playlist { get; set; }
        public string PlaylistId { get; set; }
        public Track Track { get; set; }
        public string TrackId { get; set; }
    }
}
