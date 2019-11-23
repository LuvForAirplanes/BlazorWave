using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWave.Data.Models.DTOs
{
    public class TrackUploadDTO
    {
        public int Order { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public string Extension { get; set; }
    }
}
