using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWave.Data.Models
{
    public class TrackShare : BaseModel
    {
        public IdentityUser Owner { get; set; }
        public string OwnerId { get; set; }
        public Track Track { get; set; }
        public string TrackId { get; set; }
        public IdentityUser Sharer { get; set; }
        public string SharerId { get; set; }
    }
}
