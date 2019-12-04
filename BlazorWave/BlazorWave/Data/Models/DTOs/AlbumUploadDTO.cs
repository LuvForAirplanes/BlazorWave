using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWave.Data.Models.DTOs
{
    public class AlbumUploadDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        public string AlbumArtAsPng { get; set; }
        public DateTime Released { get; set; }
        public List<TrackUploadDTO> Tracks { get; set; } = new List<TrackUploadDTO>();
    }
}
