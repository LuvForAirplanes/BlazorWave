using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWave.Data.Models
{
    public class UserSpeedPreference : BaseModel
    {
        public IdentityUser User { get; set; }
        public string UserId { get; set; }
        public Track Track { get; set; }
        public string TrackId { get; set; }
        public int Speed { get; set; }
    }
}
